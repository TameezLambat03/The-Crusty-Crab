using CrustyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrustyAPI.Data;

public static class AdminSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        const string username = "Admin2";
        const string email = "admin@crustycrab.co.za";
        const string password = "ChangeMe123!";

        bool adminExists = await context.AdminUsers
            .AnyAsync(a => a.Username == username);

        if (adminExists)
        {
            return;
        }

        var admin = new AdminUser
        {
            Username = username,
            EmailAddress = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            StaffId = null
        };

        context.AdminUsers.Add(admin);
        await context.SaveChangesAsync();
    }
}