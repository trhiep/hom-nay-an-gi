using System.Net.Http.Headers;

namespace HomNayAnGiApp.Middlewares
{
    public class MyRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyRequestService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> CallApiAsync(string apiUrl)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await _httpClient.GetAsync(apiUrl);
        }
    }
}
