﻿using Minerva.BusinessLayer;
using Minerva.Models;
using MinervaApi.IDataAccessLayer;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace MinervaApi.ExternalApi
{
    public class KeycloakApiService : IKeycloakApiService
    {
        private readonly HttpClient _httpClient;

        public KeycloakApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> CreateUser(User? us)
        {
            try
            {
                KeyClient client = new KeyClient();
                client.id = "";
                client.email = us.Email;
                client.emailVerified = false;
                client.username = us.Email;
                client.firstName = us.FirstName;
                client.lastName = us.LastName;
                client.enabled = us.IsActive;
                client.attributes = new Attributes();
                client.attributes.AnyId = new List<string?>();
                client.attributes.AnyId.Add(us.UserId);
                client.attributes.TenantId = new List<string?>();
                client.attributes.TenantId.Add(us.TenantId.ToString());
                client.groups = new List<string>();
                client.groups.Add(us.Roles);





                client.requiredActions = ["UPDATE_PASSWORD", "VERIFY_EMAIL"];
                var request = new HttpRequestMessage();
                var jsonPayload = JsonConvert.SerializeObject(client);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("users", request.Content);
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
                }
                else
                {
                    throw new HttpRequestException($"Error creating user: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch(Exception ex)
            {
                throw new HttpRequestException($"Error Reset Password: {ex}");
            }
        }

        public async Task<List<KeyClient?>> GetUser(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync("users?username=" + email);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    List<KeyClient?>? clist = JsonConvert.DeserializeObject<List<KeyClient?>>(jsonResponse);
                    return clist;
                }
                else
                {
                    throw new HttpRequestException($"Error creating user: {await response.Content.ReadAsStringAsync()}");
                }

            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Error Reset Password: {ex}");
            }
        }
        public async Task<APIStatus?> ResetPassword(string? id, string email)
        {
            APIStatus status = new APIStatus();
            try
            {
                var request = new HttpRequestMessage();
                var response = await _httpClient.PutAsync("users/" + id + "/reset-password-email", request.Content);
                if (response.IsSuccessStatusCode)
                {
                    status = new APIStatus { Code = "200", Message = "password is reset successfully! check your registered email address" };
                    return status;
                }
                else
                {
                    throw new HttpRequestException($"Error Reset Password: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Error Reset Password: {ex}");
            }

        }
        public async Task<APIStatus?> Verifyemail(string? id, string email)
        {
            APIStatus status = new APIStatus();
            try
            {
                var request = new HttpRequestMessage();
                var response = await _httpClient.PutAsync("users/" + id + "/send-verify-email", request.Content);
                if (response.IsSuccessStatusCode)
                {
                    status = new APIStatus { Code = "200", Message = "email sent successfully! check your registered email address" };
                    return status;
                }
                else
                {
                    throw new HttpRequestException($"Error Reset Password: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Error Reset Password: {ex}");
            }

        }
        public async Task<APIStatus?> DeleteUser(string? id, string email)
        {
            APIStatus status = new APIStatus();
            try
            {
                var request = new HttpRequestMessage();
                var response = await _httpClient.DeleteAsync("users/" + id);
                if (response.IsSuccessStatusCode)
                {
                    status = new APIStatus { Code = "200", Message = "password is reset successfully! check your registered email address" };
                    return status;
                }
                else
                {
                    throw new HttpRequestException($"Error Reset Password: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Error Reset Password: {ex}");
            }

        }

        public async Task<APIStatus?> Sendemails(string recipientEmail, string CCrecipientEmail, string subject, string body)
        {
            APIStatus status = new APIStatus();
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(recipientEmail));  // Recipient email address
                message.From = new MailAddress("hiiamsanthu@gmail.com");  // Your Gmail address
                message.CC.Add(new MailAddress(CCrecipientEmail)); // CC email address
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;  // Gmail SMTP port
                    smtpClient.Credentials = new NetworkCredential("hiiamsanthu@gmail.com", "Donthi@0805");  // Your Gmail credentials
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(message);
                    status.Code = "200";
                    status.Message = "successfully sent email";
                }
            }
            catch (Exception ex)
            {
                status.Code = "200";
                status.Message = "Error sending email: " + ex.Message;
                // Handle exception
            }
            return status;
        }
    }
}
