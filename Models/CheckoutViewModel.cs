namespace CrustyCrab.Models
{
    public class CheckoutViewModel
    {
        public int MenuItemId { get; set; }
        public string SpiceLevel { get; set; } = "Medium";
        public List<string> Addons { get; set; } = new();
        public string FulfillmentMethod { get; set; } = "Collection";
        public string CardholderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public string Cvc { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee => FulfillmentMethod == "Delivery" ? 50.00m : 0.00m;
        public decimal Total => Subtotal + DeliveryFee;
    }
}
