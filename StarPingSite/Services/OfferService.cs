using Newtonsoft.Json;
using StarPingData.Models;

namespace StarPingSite.Services
{
    public class OfferService : BaseService
    {
        public OfferService(HttpClient httpClient) : base(httpClient) { }
        public async Task<IEnumerable<OfferModel>?> GetAsync()
        {
            var response = await _httpClient.GetStringAsync($"/Api/Offer");
            return JsonConvert.DeserializeObject<IEnumerable<OfferModel>>(response);
        }
        public async Task<IEnumerable<OfferModel>?> GetStationaryAsync()
        {
            var response = await _httpClient.GetStringAsync($"/Api/Offer/Stationary");
            return JsonConvert.DeserializeObject<IEnumerable<OfferModel>>(response);
        }
        public async Task<IEnumerable<OfferModel>?> GetMobileAsync()
        {
            var response = await _httpClient.GetStringAsync($"/Api/Offer/Mobile");
            return JsonConvert.DeserializeObject<IEnumerable<OfferModel>>(response);
        }
        public async Task<IEnumerable<OfferModel>?> GetBusinessAsync()
        {
            var response = await _httpClient.GetStringAsync($"/Api/Offer/Business");
            return JsonConvert.DeserializeObject<IEnumerable<OfferModel>>(response);
        }
        public async Task<OfferModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"/Api/Offer/{id}");
            return JsonConvert.DeserializeObject<OfferModel>(response);
        }
        public async Task<OfferModel?> SaveAsync(OfferModel offer)
        {
            var response = await _httpClient.PostAsJsonAsync($"/Api/Offer", offer);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<OfferModel>();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/Api/Offer/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
