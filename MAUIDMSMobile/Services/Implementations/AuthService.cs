using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using MAUIDMSMobile.Services;
using MAUIDMSMobile.Resx;

namespace YourApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly string tokenUrl = "https://login.microsoftonline.com/6db6dfc0-0dec-4ea8-8818-5d7d08b50ca4/oauth2/v2.0/token";
        private readonly string clientId = "df359414-8df5-4893-a9f8-8efae4dbc9bc";
        private readonly string clientSecret = "vG98Q~zEeZmOjSRbbvo3oDhelWAG25H7gRG.Ha.N";
        private readonly string scope = "https://api.businesscentral.dynamics.com/.default";

        public async Task<string> GetAccessTokenAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("scope", scope),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });

                    HttpResponseMessage response = await client.PostAsync(tokenUrl, content);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        return tokenResponse.access_token;
                    }
                    else
                    {
                        throw new Exception("Error fetching access token: " + responseContent);
                    }
                }
            }
            catch (Exception x)
            {

                throw;
            }
        }
    }
}
