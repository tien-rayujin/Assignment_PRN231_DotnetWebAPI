using Azure;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace WebClient_CS
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> SendAuthRequest<TRequest, TResponse>(string requestUri, TRequest requestData)
        {
            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
                return JsonConvert.DeserializeObject<TResponse>(responseContent);
            }

            return default(TResponse);
        }

        public async Task SendPostRequest<TRequest>(string requestUri, TRequest requestData, string? token)
        {
            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content,
            };
            if (!token.IsNullOrEmpty())
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            await _httpClient.SendAsync(request);
        }

        public async Task SendPutRequest<TRequest>(string requestUri, TRequest requestData, string? token)
        {
            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(requestUri),
                Content = content,
            };
            if (!token.IsNullOrEmpty())
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            await _httpClient.SendAsync(request);
        }

        public async Task SendDeleteRequest(string requestUri, string? token)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri),
            };
            if (!token.IsNullOrEmpty())
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            await _httpClient.SendAsync(request);
        }

        public string O2Json<ObjectType>(ObjectType obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public ObjectType Json2O<ObjectType>(string json)
        {
            return JsonConvert.DeserializeObject<ObjectType>(json);
        }

        public async Task<T> SendGetRequest<T>(string requestUri, string? token)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri),
            };
            if(!token.IsNullOrEmpty())
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }

            return default(T);
        }
    }

}
