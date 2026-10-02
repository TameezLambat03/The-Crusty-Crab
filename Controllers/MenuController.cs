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

        internal MenuItem ToMenuItem(ApiMenuItem item)
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApiMenuItem item)
        {
            var created = await _api.CreateMenuItem(item);
            return created == null ? BadRequest() : Json(created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] ApiMenuItem item)
        {
            item.MenuItemID = id;
            var updated = await _api.UpdateMenuItem(item);
            return updated == null ? NotFound() : Json(updated);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return await _api.DeleteMenuItem(id) ? NoContent() : NotFound();
        }

        public IActionResult Review()
        {
            return View();
        }
    }
}
