using CrustyCrab.Models;
using CrustyCrab.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrustyCrab.Controllers
{
    public class AuthController : Controller
    {
        private readonly CrustyApiClient _api;

        public AuthController(CrustyApiClient api)
        {
            _api = api;
        }

        [HttpPost]
        public async Task<IActionResult> CustomerLogin([FromBody] LoginRequest request)
        {
            var response = await _api.CustomerLogin(request.Identifier, request.Password);
            return response == null ? StatusCode(502, new { message = "Authentication service is unavailable." }) : Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin([FromBody] LoginRequest request)
        {
            var response = await _api.AdminLogin(request.Identifier, request.Password);
            return response == null ? StatusCode(502, new { message = "Authentication service is unavailable." }) : Json(response);
        }
    }
}
