using Newtonsoft.Json;
using StarPingData.Models;

namespace StarPingSite.Services
{
    public class OrderService : BaseService
    {
        public OrderService(HttpClient httpClient) : base(httpClient) { }

        public async Task<OrderModel?> GetOrderByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"Api/Order/{id}");
            return JsonConvert.DeserializeObject<OrderModel>(response);
        }
        public async Task<OrderModel?> GetOrderDetailsByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"Api/Order/Items/{id}");
            return JsonConvert.DeserializeObject<OrderModel>(response);
        }
        public async Task<IEnumerable<OrderModel>?> GetOrdersAsync()
        {
            var response = await _httpClient.GetStringAsync($"/Api/Order");
            return JsonConvert.DeserializeObject<IEnumerable<OrderModel>>(response);
        }
        public async Task<IEnumerable<OrderModel>?> GetFinalizedOrdersAsync()
        {
            var response = await _httpClient.GetStringAsync($"/Api/Order/Finalized");
            return JsonConvert.DeserializeObject<IEnumerable<OrderModel>>(response);
        }
        public async Task<bool?> FinalizeAsync(int id)
        {
            var response = await _httpClient.PostAsJsonAsync($"Api/Order/Finalize/{id}", (object)null);
            return response.IsSuccessStatusCode;
        }
    }
}
