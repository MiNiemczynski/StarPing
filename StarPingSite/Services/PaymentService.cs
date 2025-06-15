using Newtonsoft.Json;
using StarPingData.Models;

namespace StarPingSite.Services
{
    public class PaymentService : BaseService
    {
        public PaymentService(HttpClient httpClient) : base(httpClient) { }

        public async Task<IEnumerable<PaymentModel>?> GetOrderPaymentsAsync(int orderId)
        {
            var response = await _httpClient.GetStringAsync($"/Api/Payment/order/{orderId}");
            return JsonConvert.DeserializeObject<IEnumerable<PaymentModel>>(response);
        }
        public async Task<PaymentModel?> PayAsync(int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"/Api/Payment/{id}", (object)null);

            //Debug.WriteLine($" >> Status code: {response.StatusCode} | Content: {response.Content.ReadAsStringAsync()}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<PaymentModel>();
        }
    }
}
