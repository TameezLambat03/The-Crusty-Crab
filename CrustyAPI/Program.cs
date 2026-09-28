using CrustyAPI.Data;
using CrustyAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<DeliveryAddressService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<PaymentService>();
            builder.Services.AddScoped<AdminStaffService>();
            builder.Services.AddScoped<CrabbyBotService>();
            var app = builder.Build();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "Images")),
                RequestPath = "/Images"
            });

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}