using Newtonsoft.Json;
using StarPingData.Models;

namespace StarPingSite.Services
{
    public class DeviceService : BaseService
    {
        public DeviceService(HttpClient httpClient) : base(httpClient) { }
        public async Task<IEnumerable<DeviceModel>?> GetAsync()
        {
            var response = await _httpClient.GetStringAsync($"/Api/Device");
            return JsonConvert.DeserializeObject<IEnumerable<DeviceModel>>(response);
        }
        public async Task<IEnumerable<DeviceModel>?> GetMobileAsync()
        {
            var response = await _httpClient.GetStringAsync($"/Api/Device/Mobile");
            return JsonConvert.DeserializeObject<IEnumerable<DeviceModel>>(response);
        }
        public async Task<IEnumerable<DeviceModel>?> GetStationaryAsync()
        {
            var response = await _httpClient.GetStringAsync($"/Api/Device/Stationary");
            return JsonConvert.DeserializeObject<IEnumerable<DeviceModel>>(response);
        }
        public async Task<DeviceModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"/Api/Device/{id}");
            return JsonConvert.DeserializeObject<DeviceModel>(response);
        }
        public async Task<DeviceModel?> SaveAsync(DeviceModel device)
        {
            var response = await _httpClient.PostAsJsonAsync($"/Api/Device", device);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<DeviceModel>();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/Api/Device{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
