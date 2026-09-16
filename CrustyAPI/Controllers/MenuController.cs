using CrustyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrustyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public IActionResult GetMenu()
        {
            return Ok(_menuService.GetMenu());
        }
    }
}