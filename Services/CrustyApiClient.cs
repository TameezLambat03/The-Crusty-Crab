using System.Net.Http.Json;
using CrustyCrab.Models;

namespace CrustyCrab.Services
{
    public class CrustyApiClient
    {
        private readonly HttpClient _http;
        private readonly ILogger<CrustyApiClient> _logger;

        public CrustyApiClient(HttpClient http, ILogger<CrustyApiClient> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<ApiMenuItem>?> GetMenu()
        {
            return await Get<List<ApiMenuItem>>("api/menu");
        }

        public async Task<ApiMenuItem?> GetMenuItem(int id)
        {
            return await Get<ApiMenuItem>($"api/menu/{id}");
        }

        public async Task<bool> UpdateMenuItem(ApiMenuItem item)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/menu/{item.MenuItemID}", item);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Could not update menu item {Id}", item.MenuItemID);
                return false;
            }
        }

        public async Task<List<ApiOrder>?> GetOrders()
        {
            return await Get<List<ApiOrder>>("api/order");
        }

        public async Task<ApiOrder?> CreateOrder(int customerId, string deliveryMethod, int menuItemId, int quantity, string? notes)
        {
            var body = new
            {
                CustomerID = customerId,
                DeliveryMethod = deliveryMethod,
                Items = new[] { new { MenuItemID = menuItemId, Quantity = quantity, Notes = notes } }
            };
            return await Post<ApiOrder>("api/order", body);
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/order/{orderId}/status", new { OrderStatus = status });
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Could not update order {Id}", orderId);
                return false;
            }
        }

        public async Task<ApiCustomer?> CreateCustomer(string firstName, string lastName, string phone, string email, string password)
        {
            var body = new { FirstName = firstName, LastName = lastName, PhoneNumber = phone, EmailAddress = email, Password = password };
            return await Post<ApiCustomer>("api/customer", body);
        }

        public async Task<List<ApiPaymentMethod>?> GetPaymentMethods()
        {
            return await Get<List<ApiPaymentMethod>>("api/payment/methods");
        }

        public async Task<ApiPaymentMethod?> CreatePaymentMethod(string name)
        {
            return await Post<ApiPaymentMethod>("api/payment/methods", new { MethodName = name });
        }

        public async Task<ApiPayment?> CreatePayment(int orderId, int paymentMethodId, decimal amount)
        {
            return await Post<ApiPayment>("api/payment", new { OrderID = orderId, PaymentMethodID = paymentMethodId, Amount = amount });
        }

        public async Task<bool> UpdatePaymentStatus(int paymentId, string status)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/payment/{paymentId}/status", new { PaymentStatus = status });
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Could not update payment {Id}", paymentId);
                return false;
            }
        }

        private async Task<T?> Get<T>(string url) where T : class
        {
            try
            {
                var response = await _http.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
            {
                _logger.LogWarning(ex, "GET {Url} failed", url);
                return null;
            }
        }

        private async Task<T?> Post<T>(string url, object body) where T : class
        {
            try
            {
                var response = await _http.PostAsJsonAsync(url, body);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
            {
                _logger.LogWarning(ex, "POST {Url} failed", url);
                return null;
            }
        }

        public string ResolveApiUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            if (Uri.TryCreate(url, UriKind.Absolute, out var absolute))
                return absolute.ToString();

            return new Uri(_http.BaseAddress!, url.TrimStart('/')).ToString();
        }
    }
}
