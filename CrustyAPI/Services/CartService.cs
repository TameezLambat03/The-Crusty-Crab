using CrustyAPI.Models;

namespace CrustyAPI.Services
{
    public class CartService
    {
        private readonly Dictionary<int, List<CartItem>> _carts = new();

        private readonly List<MenuItem> _menuItems = new()
        {
            new MenuItem
            {
                Id = 1,
                Name = "Krabby Patty",
                Price = 49.99m
            },
            new MenuItem
            {
                Id = 2,
                Name = "Double Krabby Patty",
                Price = 69.99m
            },
            new MenuItem
            {
                Id = 3,
                Name = "Kelp Fries",
                Price = 24.99m
            },
            new MenuItem
            {
                Id = 4,
                Name = "Krabby Shake",
                Price = 29.99m
            }
        };

        public List<CartItem> GetCart(int userId)
        {
            if (!_carts.ContainsKey(userId))
            {
                _carts[userId] = new List<CartItem>();
            }

            return _carts[userId];
        }

        public CartItem? AddItem(int userId, int menuItemId, int quantity)
        {
            var menuItem = _menuItems.FirstOrDefault(x => x.Id == menuItemId);

            if (menuItem == null)
                return null;

            var cart = GetCart(userId);

            var existingItem = cart.FirstOrDefault(x => x.MenuItemId == menuItemId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                return existingItem;
            }

            var cartItem = new CartItem
            {
                Id = cart.Count + 1,
                UserId = userId,
                MenuItemId = menuItem.Id,
                ItemName = menuItem.Name,
                UnitPrice = menuItem.Price,
                Quantity = quantity
            };

            cart.Add(cartItem);

            return cartItem;
        }

        public CartItem? UpdateItem(int userId, int itemId, int quantity)
        {
            var cart = GetCart(userId);

            var item = cart.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
                return null;

            item.Quantity = quantity;

            return item;
        }

        public bool RemoveItem(int userId, int itemId)
        {
            var cart = GetCart(userId);

            var item = cart.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
                return false;

            cart.Remove(item);

            return true;
        }

        public void ClearCart(int userId)
        {
            GetCart(userId).Clear();
        }
    }
}