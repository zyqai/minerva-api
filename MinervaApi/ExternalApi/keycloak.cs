using Minerva.BusinessLayer;
using Minerva.Models;
using Newtonsoft.Json;
using System.Text;

namespace MinervaApi.ExternalApi
{
    public class Keycloak
    {
        private string KeycloakBaseURL = "https://login.dev.minerva.zyq.ai/auth/realms/minerva/protocol/openid-connect/token";

        public async Task<tokenResult?> GetToken()
        {
            tokenResult? res = new tokenResult();
            var requestContent = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", "minerva-frontend" },
                { "username", "rajendra" },
                { "password", "Prasads" }
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

        public class KeyClientOpr
        {
            public async Task<string> ClientInsert(KeyClient client)
            {
                try
                {
                    Keycloak keycloak = new Keycloak();
                    tokenResult result = await keycloak.GetToken();
                    string authorizationKey = "Bearer " + result.access_token;
                    string userCreationEndpoint = "http://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users";
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
            public async Task<List<KeyClient>> KeyClockClientGet(string email)
            {
                List<KeyClient> clist = new List<KeyClient>();
                Keycloak keycloak = new Keycloak();
                tokenResult result = await keycloak.GetToken();
                string authorizationKey = "Bearer " + result.access_token;

                string userGetEndpoint = "http://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users?username="+email;
                using (HttpClient client = new HttpClient())
                {
                    // Set headers
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

                    // Make GET request
                    HttpResponseMessage response = await client.GetAsync(userGetEndpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read and display the content
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        clist= JsonConvert.DeserializeObject<List<KeyClient>>(jsonResponse);
                    }
                    else
                    {
                        throw new HttpRequestException($"Error creating user: {await response.Content.ReadAsStringAsync()}");
                    }
                }
                return clist;
            }
        }
    }
}
