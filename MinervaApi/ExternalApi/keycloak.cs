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
                { "client_id", "minerva-backend-local" },
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
            public async Task<string> ClientInsert(KeyClient _client)
            {
                try
                {
                    Keycloak keycloak = new Keycloak();
                    tokenResult? result = await keycloak.GetToken();
                    string userCreationEndpoint = "https://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users";
                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(HttpMethod.Post, userCreationEndpoint))
                        {
                            var jsonPayload = JsonConvert.SerializeObject(_client);
                            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                            request.Headers.Add("Accept", "*/*");
                            request.Headers.Add("Authorization", $"Bearer {result?.access_token}");

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

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public async Task<List<KeyClient?>> KeyClockClientGet(string email)
            {
                List<KeyClient>? clist = new List<KeyClient>();
                Keycloak keycloak = new Keycloak();
                tokenResult result = await keycloak.GetToken();
                string userGetEndpoint = "https://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users?username=" + email;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {result?.access_token}");
                    HttpResponseMessage response = await client.GetAsync(userGetEndpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        clist = JsonConvert.DeserializeObject<List<KeyClient>>(jsonResponse);
                    }
                    else
                    {
                        throw new HttpRequestException($"Error creating user: {await response.Content.ReadAsStringAsync()}");
                    }
                }
                return clist;
            }
            public async Task<APIStatus> ResetPassword(string? id, string? email)
            {
                List<KeyClient> clist = new List<KeyClient>();
                Keycloak keycloak = new Keycloak();
                tokenResult? result = await keycloak.GetToken();

                APIStatus status = new APIStatus();
                try
                {
                    string apiUrl = "https://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users/" + id + "/reset-password-email";

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Accept", "*/*");
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {result?.access_token}");
                        HttpResponseMessage response = await client.PutAsync(apiUrl, null);
                        if (response.IsSuccessStatusCode)
                        {
                            status.Code = "201";
                            status.Message = "Password reset email sent successfully.";
                        }
                        else
                        {
                            throw new HttpRequestException($"Error creating user: {await response.Content.ReadAsStringAsync()}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }

                return status;
            }
            public async Task<APIStatus> sendverifyemail(string? id, string? email)
            {
                List<KeyClient> clist = new List<KeyClient>();
                Keycloak keycloak = new Keycloak();
                tokenResult? result = await keycloak.GetToken();
                APIStatus status = new APIStatus();
                try
                {
                    string apiUrl = "https://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users/" + id + "/send-verify-email";
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Accept", "*/*");
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {result?.access_token}");
                        HttpResponseMessage response = await client.PutAsync(apiUrl, null);
                        if (response.IsSuccessStatusCode)
                        {
                            status.Code = "201";
                            status.Message = "verify email sent successfully.";
                        }
                        else
                        {
                            throw new HttpRequestException($"Error creating user: {await response.Content.ReadAsStringAsync()}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return status;
            }

            public async Task<APIStatus> keyclockclientDelete(string? id, string? email)
            {
                List<KeyClient> clist = new List<KeyClient>();
                Keycloak keycloak = new Keycloak();
                tokenResult? result = await keycloak.GetToken();
                APIStatus status = new APIStatus();
                try
                {
                    string apiUrl = "https://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users/" + id;
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Accept", "*/*");
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {result?.access_token}");
                        HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            status.Code = "201";
                            status.Message = "deleted successfully.";
                        }
                        else
                        {
                            throw new HttpRequestException($"Error creating user: {await response.Content.ReadAsStringAsync()}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return status;
            }
        }
    }
}
