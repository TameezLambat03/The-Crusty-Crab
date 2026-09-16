namespace CrustyAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MenuItemId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;
    }
}