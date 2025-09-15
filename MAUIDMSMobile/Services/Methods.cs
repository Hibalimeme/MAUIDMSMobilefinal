using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Services
{
    public class Methods
    {
        private readonly HttpClient _httpClient;

        public Methods(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendPostRequestAsync(string relativeUrl, string jsonPayload, string token)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Post, relativeUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                    return content;

                return $"Error: {response.StatusCode}\nDetails: {content}";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        public async Task<string> SendGetRequestAsync(string relativeUrl, string token)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return response.IsSuccessStatusCode
                    ? content
                    : $"Error: {response.StatusCode}\nDetails: {content}";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}
