using CrustyCrab.Models;
using CrustyCrab.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrustyCrab.Controllers
{
    public class CartController : Controller
    {
        private readonly CrustyApiClient _api;

        public CartController(CrustyApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> Customer(int customerId)
        {
            var cart = await _api.GetCustomerCart(customerId);
            return cart == null ? NotFound() : Json(cart);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var cart = await _api.GetCart(id);
            return cart == null ? NotFound() : Json(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartRequest request)
        {
            var cart = await _api.CreateCart(request.CustomerID);
            return cart == null ? BadRequest() : Json(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int cartId, [FromBody] CartItemRequest request)
        {
            var cart = await _api.AddCartItem(cartId, request.MenuItemID, request.Quantity);
            return cart == null ? BadRequest() : Json(cart);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(int cartItemId, [FromBody] CartItemUpdateRequest request)
        {
            var cart = await _api.UpdateCartItem(cartItemId, request.Quantity);
            return cart == null ? NotFound() : Json(cart);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var cart = await _api.RemoveCartItem(cartItemId);
            return cart == null ? NotFound() : Json(cart);
        }
    }
}
