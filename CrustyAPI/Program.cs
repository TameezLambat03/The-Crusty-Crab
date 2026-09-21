using CrustyAPI.Data;
using CrustyAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CrustyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddOpenApi();
            builder.Services.AddControllers();

            // Database
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                ));

            // API services
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<MenuService>();
            //builder.Services.AddSingleton<CartService>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<DeliveryAddressService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}