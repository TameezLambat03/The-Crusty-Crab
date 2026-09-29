using Microsoft.AspNetCore.Mvc;
using CrustyCrab.Models;
using CrustyCrab.Services;

namespace CrustyCrab.Controllers
{
    public class CheckoutController : Controller
    {
        private static readonly Dictionary<string, decimal> AddonPrices = new()
        {
            { "Extra Jumbo Prawns (5pcs)", 45.00m },
            { "Extra Corn on the Cob (2pcs)", 25.00m }
        };

        private readonly CrustyApiClient _api;

        public CheckoutController(CrustyApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index(int? menuItemId, string? spiceLevel, List<string>? addons)
        {
            var model = new CheckoutViewModel
            {
                SpiceLevel = string.IsNullOrWhiteSpace(spiceLevel) ? "Medium" : spiceLevel,
                Addons = addons ?? new List<string>()
            };

            ApiMenuItem? item = null;
            if (menuItemId.HasValue)
            {
                item = await _api.GetMenuItem(menuItemId.Value);
            }
            if (item == null)
            {
                var menu = await _api.GetMenu();
                item = menu?.FirstOrDefault(m => m.Availability);
            }

            if (item != null)
            {
                model.MenuItemId = item.MenuItemID;
                model.Subtotal = item.Price + AddonTotal(model.Addons);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizePayment(CheckoutViewModel model)
        {
            var item = await _api.GetMenuItem(model.MenuItemId);
            if (item == null || !item.Availability)
            {
                TempData["Error"] = "That item is no longer available.";
                return RedirectToAction(nameof(Index));
            }

            var nameParts = (model.CardholderName ?? string.Empty).Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts.Length > 0 ? nameParts[0] : "Guest";
            var lastName = nameParts.Length > 1 ? nameParts[1] : "Customer";
            var guestKey = Guid.NewGuid().ToString("N");

            var customer = await _api.CreateCustomer(
                firstName,
                lastName,
                "0000000000",
                $"guest-{guestKey}@crustycrab.local",
                guestKey);

            if (customer == null)
            {
                TempData["Error"] = "We could not create your order. Please try again.";
                return RedirectToAction(nameof(Index), new { menuItemId = model.MenuItemId, spiceLevel = model.SpiceLevel });
            }

            var notes = $"Spice: {model.SpiceLevel}";
            if (model.Addons != null && model.Addons.Count > 0)
            {
                notes += "; Add-ons: " + string.Join(", ", model.Addons);
            }

            var order = await _api.CreateOrder(customer.CustomerID, model.FulfillmentMethod, item.MenuItemID, 1, notes);
            if (order == null)
            {
                TempData["Error"] = "We could not place your order. Please try again.";
                return RedirectToAction(nameof(Index), new { menuItemId = model.MenuItemId, spiceLevel = model.SpiceLevel });
            }

            var methods = await _api.GetPaymentMethods() ?? new List<ApiPaymentMethod>();
            var method = methods.FirstOrDefault(m => m.MethodName.Contains("Card", StringComparison.OrdinalIgnoreCase))
                         ?? methods.FirstOrDefault()
                         ?? await _api.CreatePaymentMethod("Card");

            if (method == null)
            {
                TempData["Error"] = "Payment is unavailable right now. Please try again.";
                return RedirectToAction(nameof(Index), new { menuItemId = model.MenuItemId, spiceLevel = model.SpiceLevel });
            }

            var subtotal = item.Price + AddonTotal(model.Addons);
            var fee = model.FulfillmentMethod == "Delivery" ? 50.00m : 0.00m;

            var payment = await _api.CreatePayment(order.OrderID, method.PaymentMethodID, subtotal + fee);
            if (payment == null)
            {
                TempData["Error"] = "Your payment could not be processed. Please try again.";
                return RedirectToAction(nameof(Index), new { menuItemId = model.MenuItemId, spiceLevel = model.SpiceLevel });
            }

            await _api.UpdatePaymentStatus(payment.PaymentID, "Paid");

            return RedirectToAction("Review", "Menu");
        }

        private static decimal AddonTotal(IEnumerable<string>? addons)
        {
            return (addons ?? Enumerable.Empty<string>()).Sum(a => AddonPrices.TryGetValue(a, out var price) ? price : 0m);
        }
    }
}
