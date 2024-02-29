using Minerva.Models;
using MinervaApi.ExternalApi;

namespace MinervaApi.IDataAccessLayer
{
    public interface IKeycloakApiService
    {
       // public Task<HttpResponseMessage> GetUsers();
        public Task<HttpResponseMessage> CreateUser(User? us);
        public Task<List<KeyClient?>> GetUser(string emailid);
        public Task<APIStatus?> ResetPassword(string? id, string emailid);
        public Task<APIStatus?> Verifyemail(string? id, string emailid);
        public Task<APIStatus?> DeleteUser(string? id, string emailid);



    }
}
