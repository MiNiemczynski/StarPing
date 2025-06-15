using Newtonsoft.Json;
using StarPingData.Models;

namespace StarPingSite.Services
{
    public class AddressService : BaseService
    {
        public AddressService(HttpClient httpClient) : base(httpClient) { }
        public async Task<AddressModel?> GetByOrderIdAsync(int orderId)
        {
            var response = await _httpClient.GetStringAsync($"Api/Address/order/{orderId}");
            return JsonConvert.DeserializeObject<AddressModel>(response);
        }
        public async Task<AddressModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"Api/Address/address/{id}");
            return JsonConvert.DeserializeObject<AddressModel>(response);
        }
        public async Task<bool?> SaveAsync(AddressModel address, int orderId = 0)
        {
            var response = await _httpClient.PostAsJsonAsync($"Api/Address/save?orderId={orderId}", address);
            return response.IsSuccessStatusCode;
        }
    }
}
