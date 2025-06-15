using Newtonsoft.Json;
using StarPingData.Models;
using StarPingData.Models.Cart;
using System.Diagnostics;

namespace StarPingSite.Services
{
    public class WebCartService : BaseService
    {
        string CartSessionId;

        public WebCartService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient)
        {
            CartSessionId = GetIdCartSession(httpContextAccessor.HttpContext);
        }

        private string GetIdCartSession(HttpContext httpContext)
        {
            if (string.IsNullOrWhiteSpace(httpContext.Session.GetString("IdCartSession")))
            {
                if (!string.IsNullOrEmpty(httpContext.User?.Identity?.Name))
                {
                    httpContext.Session.SetString("IdCartSession", httpContext.User.Identity.Name);
                }
                else
                {
                    var tempIdCartSession = Guid.NewGuid();
                    httpContext.Session.SetString("IdCartSession", tempIdCartSession.ToString());
                }
            }
            return httpContext.Session.GetString("IdCartSession") ?? "";
        }

        public async Task<CartDataModel?> GetCartData()
        {
            var items = JsonConvert.DeserializeObject<List<CartItemModel>>
                (await _httpClient.GetStringAsync($"Api/Cart/Items/{CartSessionId}"));
            var total = JsonConvert.DeserializeObject<Dictionary<int, decimal>>
                (await _httpClient.GetStringAsync($"Api/Cart/Total/{CartSessionId}"));

            return new CartDataModel
            {
                Total = total ?? [],
                CartItems = items ?? []
            };
        }
        public async Task<bool> AddDeviceToCart(int id)
        {
            Debug.WriteLine($">> Adding device with ID {id} to cart with session ID {CartSessionId}");
            var response = await _httpClient.PostAsJsonAsync($"Api/Cart/{CartSessionId}/Device/{id}", (object)null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddSubscriptionToCart(int id, int months)
        {
            var response = await _httpClient.PostAsJsonAsync($"Api/Cart/{CartSessionId}/Offer/{id}/{months}", (object)null);
            return response.IsSuccessStatusCode;
        }
        public async Task<OrderModel?> CreateOrderAsync()
        {
            var response = await _httpClient.PostAsJsonAsync($"Api/Cart/Checkout/{CartSessionId}", (object)null);
            return await response.Content.ReadFromJsonAsync<OrderModel?>();
        }
        public async Task<bool> DeleteItemFromCartAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Api/Cart/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
