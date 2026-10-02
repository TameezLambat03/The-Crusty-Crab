using CrustyAPI.Models;
using CrustyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrustyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly MenuService _menuService;

    public MenuController(MenuService menuService)
    {
        _menuService = menuService;
    }

    // GET: api/menu
    [HttpGet]
    public async Task<IActionResult> GetMenu()
    {
        var menu = await _menuService.GetMenu();

        return Ok(menu);
    }

    // POST: api/menu
    [HttpPost]
    public async Task<IActionResult> CreateMenuItem(MenuItem menuItem)
    {
        var createdItem = await _menuService.CreateMenuItem(menuItem);

        return CreatedAtAction(
            nameof(GetMenuItem),
            new { id = createdItem.MenuItemID },
            createdItem
        );
    }

    // GET: api/menu/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuItem(int id)
    {
        var menuItem = await _menuService.GetMenuItemById(id);

        if (menuItem == null)
        {
            return NotFound(new { message = "Menu item not found" });
        }

        return Ok(menuItem);
    }

    // PUT: api/menu/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem(
        int id,
        MenuItem menuItem)
    {
        var updatedItem = await _menuService.UpdateMenuItem(id, menuItem);

        if (updatedItem == null)
        {
            return NotFound(new { message = "Menu item not found" });
        }

        return Ok(updatedItem);
    }
    //DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id)
    {
        var result = await _menuService.DeleteMenuItem(id);

        if (result.Blocked)
        {
            return Conflict(new
            {
                message = "This menu item cannot be deleted because it is used in an existing cart or order. Mark it as unavailable instead."
            });
        }

        if (!result.Success)
        {
            return NotFound(new
            {
                message = "Menu item not found"
            });
        }

        return NoContent();
    }
}