using CrustyAPI.Models;

namespace CrustyAPI.Services
{
    public class MenuService
    {
        private readonly List<MenuItem> _menuItems = new()
        {
            new MenuItem
            {
                Id = 1,
                Name = "Krabby Patty",
                Description = "The classic CrustyCrab burger.",
                Price = 49.99m,
                Category = "Burgers"
            },
            new MenuItem
            {
                Id = 2,
                Name = "Double Krabby Patty",
                Description = "A double-patty version of the classic.",
                Price = 69.99m,
                Category = "Burgers"
            },
            new MenuItem
            {
                Id = 3,
                Name = "Kelp Fries",
                Description = "Crispy seasoned fries.",
                Price = 24.99m,
                Category = "Sides"
            },
            new MenuItem
            {
                Id = 4,
                Name = "Krabby Shake",
                Description = "A creamy milkshake.",
                Price = 29.99m,
                Category = "Drinks"
            }
        };

        public List<MenuItem> GetMenu()
        {
            return _menuItems;
        }
    }
}