using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Minerva.Models;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Text;

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

        private readonly IConfiguration _configuration;

        public Keycloak(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        public async Task<tokenResult> GetToken()
        {
            string keycloakBaseURL = _configuration["KeycloakBaseURL"];
            string clientId = _configuration["client_id"];
            string username = _configuration["username"];
            string password = _configuration["password"];

            var requestContent = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", clientId },
                { "username",  username },
                { "password", password }
            };

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, keycloakBaseURL)
                {
                    Content = new FormUrlEncodedContent(requestContent)
                };
                request.Content.Headers.Clear();
                request.Content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<tokenResult>(responseBody);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error getting token: {errorMessage}");
                }
            }
        }
    }

    public class KeyClient
    {
        public string id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public bool enabled { get; set; }
        public bool emailVerified { get; set; }
        public ClientRoles clientRoles { get; set; }
        public List<object> realmRoles { get; set; }
        public List<string> requiredActions { get; set; }


    }
    public class ClientRoles
    {

    }
    public class KeyClientCrud
    {
        public async Task<string> ClientInsert(KeyClient client)
        {
            try
            {
                Keycloak keycloak = new Keycloak(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
                tokenResult result = await keycloak.GetToken();

                string authorizationKey = "Bearer " + result.access_token;
                string userCreationEndpoint = "http://localhost:8080/admin/realms/DEV/users";

                using (var httpClient = new HttpClient())
                {
                    var jsonPayload = JsonConvert.SerializeObject(client);
                    var request = new HttpRequestMessage(HttpMethod.Post, userCreationEndpoint)
                    {
                        Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                    };
                    request.Headers.Add("Accept", "*/*");
                    request.Headers.Add("Authorization", authorizationKey);

                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new HttpRequestException($"Error creating user: {await response.Content.ReadAsStringAsync()}");
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
