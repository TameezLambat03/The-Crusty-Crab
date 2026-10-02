using Microsoft.AspNetCore.Mvc;
using CrustyCrab.Models;
using CrustyCrab.Services;

namespace CrustyCrab.Controllers
{
    public class MenuController : Controller
    {
        private readonly CrustyApiClient _api;

        public MenuController(CrustyApiClient api)
        {
            _api = api;
        }

        private MenuItem ToMenuItem(ApiMenuItem item)
        {
            return new MenuItem
            {
                Id = item.MenuItemID,
                Name = item.ItemName,
                Category = item.Category,
                Description = item.Description ?? string.Empty,
                Price = item.Price,
                IsInStock = item.Availability,
                ImageUrl = _api.ResolveApiUrl(item.ImageUrl)
            };
        }

        //private static string ResolveImage(string? url)
        //{
        //    if (string.IsNullOrWhiteSpace(url))
        //    {
        //        return string.Empty;
        //    }
        //    if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase) || url.StartsWith("~/"))
        //    {
        //        return url;
        //    }
        //    if (url.StartsWith("/"))
        //    {
        //        return "~" + url;
        //    }
        //    return "~/images/" + url;
        //}

        public async Task<IActionResult> Index(string category = "All Items")
        {
            var apiItems = await _api.GetMenu();
            if (apiItems == null)
            {
                ViewData["ApiError"] = "The menu is unavailable right now. Please try again shortly.";
            }

            var items = (apiItems ?? new List<ApiMenuItem>()).Select(ToMenuItem).ToList();

            if (category != "All Items")
            {
                items = items.Where(m => m.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewData["CurrentCategory"] = category;
            return View(items);
        }

        public async Task<IActionResult> Customise(int id)
        {
            var apiItem = await _api.GetMenuItem(id);
            if (apiItem == null)
            {
                var menu = await _api.GetMenu();
                apiItem = menu?.FirstOrDefault(m => m.Availability) ?? menu?.FirstOrDefault();
            }
            if (apiItem == null)
            {
                TempData["Error"] = "That item could not be loaded.";
                return RedirectToAction(nameof(Index));
            }
            return View(ToMenuItem(apiItem));
        }

        public IActionResult Review()
        {
            return View();
        }
    }
}
