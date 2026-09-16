using CrustyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using CrustyAPI.DTOs;
using CrustyAPI.Services;

namespace RestaurantSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var response = _authService.Login(request);

        if (!response.Success)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }
}