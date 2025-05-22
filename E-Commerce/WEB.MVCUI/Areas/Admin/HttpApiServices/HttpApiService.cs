
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WEB.MVCUI.Areas.Admin.HttpApiServices
{
    public class HttpApiService : IHttpApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory= httpClientFactory;
        }
        public async Task<T> DeleteData<T>(string endpoint, string token = null)
        {
            var baseAdress = "http://localhost:5293/api/";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{baseAdress}{endpoint}"),
                Headers =
                {
                    {HeaderNames.Accept,"application/json"}
                }
            };

            if (string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            //if (responseMessage.IsSuccessStatusCode) {

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> GetData<T>(string endpoint, string token = null)
        {
            var baseAdress = "http://localhost:5293/api/";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{baseAdress}{endpoint}"),
                Headers =
                {
                    {HeaderNames.Accept,"application/json"}
                }
            };

            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            //if (responseMessage.IsSuccessStatusCode) {

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

           var response = JsonSerializer.Deserialize<T>(jsonResponse,new JsonSerializerOptions { PropertyNameCaseInsensitive= true});

            return response;
        }

        public async Task<T> PostData<T>(string endpoint, string jsonData, string token = null)
        {
            var baseAdress = "http://localhost:5293/api/";

            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{baseAdress}{endpoint}"),
                Headers =
                {
                    {HeaderNames.Accept,"application/json"}
                },
                Content = new StringContent(jsonData,Encoding.UTF8,"application/json")
            };

            if (string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            //if (responseMessage.IsSuccessStatusCode) {

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }
    }
}
