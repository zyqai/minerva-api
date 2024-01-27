using Minerva.Models;
using Newtonsoft.Json;

namespace MinervaApi.ExternalApi
{
    public class tokenResult
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }

        [JsonProperty("not-before-policy")]
        public int notbeforepolicy { get; set; }
        public string session_state { get; set; }
        public string scope { get; set; }
    }
    public class Keycloak
    {
        private string KeycloakBaseURL="http://localhost:8080/realms/DEV/protocol/openid-connect/token";

        public async Task<tokenResult?>  GetToken()
        {
            tokenResult ?res = new tokenResult();
            var requestContent = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", "minerva" },
                { "username", "admin" },
                { "password", "admin" }
            };
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, KeycloakBaseURL))
                {
                    request.Content = new FormUrlEncodedContent(requestContent);
                    request.Content.Headers.Clear();
                    request.Content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<tokenResult>(responseBody);
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<tokenResult>(errorMessage);
                    }
                }
            }
            return res;
        }
    }
}
