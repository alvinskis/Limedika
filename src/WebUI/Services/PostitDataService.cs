using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using WebUI.Interfaces;

namespace WebUI.Services
{
    public class PostitDataService : IPostitDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PostitDataService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string?> GetPostCodeByAddressAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return null;
            }
            var response = await _httpClient.GetAsync(BuildRequestUrlProperties(address));

            string responseBody = await response.Content.ReadAsStringAsync();

            return GetPostCodeFromResponse(responseBody);
        }

        private string BuildRequestUrlProperties(string address)
        {
            var query = new Dictionary<string, string>
            {
                ["address"] = address,
                ["key"] = _configuration["ExternalAPIs:PostitKey"]
            };

            return QueryHelpers.AddQueryString("", query);
        }

        private static string GetPostCodeFromResponse(string responseBody)
        {
            var parsedObject = JObject.Parse(responseBody);
            var postCodeId = parsedObject["data"]?[0]?["post_code"]?.ToString() ?? string.Empty;

            return $"LT-{postCodeId}";
        }
    }
}
