using Minerva.BusinessLayer;
using Minerva.Models;
using Newtonsoft.Json;
using System.Net;
using System;
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
                    tokenResult result = await keycloak.GetToken();
                  
                    string userCreationEndpoint = "https://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users";

                    //string json = JsonConvert.SerializeObject(_client);
                    //byte[] jsonbyte = Encoding.UTF8.GetBytes(json);

                    ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    ////ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(userCreationEndpoint);
                    //webRequest.ContentType = "application/json";
                    //webRequest.Method = "POST";
                    //webRequest.Timeout = 180000;
                    //webRequest.ContentLength = jsonbyte.Length;
                    //webRequest.Headers.Add("Authorization", $"Bearer {result.access_token}");


                    //var stream = webRequest.GetRequestStream();
                    //stream.Write(jsonbyte, 0, jsonbyte.Length);
                    //stream.Close();

                    //using (var response = webRequest.GetResponse() as HttpWebResponse)
                    //{
                    //    if (webRequest.HaveResponse && response != null)
                    //    {
                    //        using (var reader = new StreamReader(response.GetResponseStream()))
                    //        {
                    //            return reader.ReadToEnd();
                    //        }
                    //    }
                    //}
                    using (var httpClient = new HttpClient())
                    {
                        var jsonPayload = JsonConvert.SerializeObject(_client);
                        var request = new HttpRequestMessage(HttpMethod.Post, userCreationEndpoint)
                        {
                            Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                        };
                        request.Headers.Add("Accept", "*/*");
                        request.Headers.Add("Authorization", $"Bearer {result.access_token}");

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

                string userGetEndpoint = "https://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users?username="+email;
                using (HttpClient client = new HttpClient())
                {
                    // Set headers
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                   
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorizationKey}");
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

            public async Task<APIStatus> ResetPassword(string ?id, string ?email)
            {
                List<KeyClient> clist = new List<KeyClient>();
                Keycloak keycloak = new Keycloak();
                tokenResult result = await keycloak.GetToken();
                string accessToken = "Bearer " + result.access_token;
                APIStatus status = new APIStatus();
                try
                {
                    string apiUrl = "https://login.dev.minerva.zyq.ai/auth/admin/realms/minerva/users/"+id+"/reset-password-email";

                    using (HttpClient client = new HttpClient())
                    {
                        // Add headers if needed
                        client.DefaultRequestHeaders.Add("Accept", "*/*");
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                        // Create request
                        HttpResponseMessage response = await client.PutAsync(apiUrl, null);
                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            status.Code = "201";
                            status.Message="Password reset email sent successfully.";
                        }
                        else
                        {
                            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }

                return status;
            }
        }
    }
}
