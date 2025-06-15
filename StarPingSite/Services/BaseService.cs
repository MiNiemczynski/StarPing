namespace StarPingSite.Services
{
    public abstract class BaseService
    {
        protected readonly HttpClient _httpClient;

        protected BaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
