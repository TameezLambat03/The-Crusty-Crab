using CrustyAPI.Services;
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
            //api services
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<MenuService>();
            builder.Services.AddSingleton<CartService>();
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