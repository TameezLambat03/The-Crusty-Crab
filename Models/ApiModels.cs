namespace CrustyCrab.Models
{
    public class ApiMenuItem
    {
        public int MenuItemID { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool Availability { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class ApiOrderItem
    {
        public int OrderItemID { get; set; }
        public int MenuItemID { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string? Notes { get; set; }
    }

    public class ApiOrder
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public string DeliveryMethod { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int? DeliveryAddressID { get; set; }
        public int CustomerID { get; set; }
        public int? StaffId { get; set; }
        public List<ApiOrderItem> Items { get; set; } = new();
    }

    public class ApiCustomer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }

    public class ApiPaymentMethod
    {
        public int PaymentMethodID { get; set; }
        public string MethodName { get; set; } = string.Empty;
    }

    public class ApiPayment
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public int OrderID { get; set; }
    }
}
