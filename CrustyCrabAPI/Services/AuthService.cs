using RestaurantSystem.Api.DTOs;
using RestaurantSystem.Api.Models;

//handles actual login logic

//We're calling the property PasswordHash, but we're temporarily putting plaintext passwords in it purely to get the endpoint working.

//We will replace this with proper password hashing when we connect the database.

namespace RestaurantSystem.Api.Services;

public class AuthService
{
    private readonly List<User> _users = new()
    {
        new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = "admin123",
            Role = "Admin"
        },
        new User
        {
            Id = 2,
            Username = "customer",
            PasswordHash = "customer123",
            Role = "Customer"
        }
    };

    public LoginResponse Login(LoginRequest request)
    {
        var user = _users.FirstOrDefault(u =>
            u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase) &&
            u.PasswordHash == request.Password);

        if (user == null)
        {
            return new LoginResponse
            {
                Success = false,
                Message = "Invalid username or password"
            };
        }

        return new LoginResponse
        {
            Success = true,
            UserId = user.Id,
            Username = user.Username,
            Role = user.Role,
            Message = "Login successful"
        };
    }
}