using CrustyAPI.DTOs;
using CrustyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrustyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetCart(int userId)
        {
            return Ok(_cartService.GetCart(userId));
        }

        [HttpPost("{userId}")]
        public IActionResult AddItem(
            int userId,
            AddCartItemRequest request)
        {
            if (request.Quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            var item = _cartService.AddItem(
                userId,
                request.MenuItemId,
                request.Quantity);

            if (item == null)
                return NotFound("Menu item not found.");

            return Ok(item);
        }

        [HttpPut("{userId}/{itemId}")]
        public IActionResult UpdateItem(
            int userId,
            int itemId,
            UpdateCartItemRequest request)
        {
            if (request.Quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            var item = _cartService.UpdateItem(
                userId,
                itemId,
                request.Quantity);

            if (item == null)
                return NotFound("Cart item not found.");

            return Ok(item);
        }

        [HttpDelete("{userId}/{itemId}")]
        public IActionResult RemoveItem(int userId, int itemId)
        {
            var removed = _cartService.RemoveItem(userId, itemId);

            if (!removed)
                return NotFound("Cart item not found.");

            return Ok(new
            {
                message = "Item removed from cart."
            });
        }

        [HttpDelete("{userId}")]
        public IActionResult ClearCart(int userId)
        {
            _cartService.ClearCart(userId);

            return Ok(new
            {
                message = "Cart cleared."
            });
        }
    }
}