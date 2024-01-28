using Microsoft.AspNetCore.Mvc;
using Minerva.Models;
using Minerva.Models.Returns;

namespace Minerva.BusinessLayer.Interface
{
    public interface IUserBL
    {
        public Task<User?> GetUser(Models.Requests.UsersRequest user);
        public Task<List<User?>> GetALLUsers();
        public Task<string> SaveUser(Models.Requests.UsersRequest user);
        public Task<bool> UpdateUser(Models.Requests.UsersRequest user);
        public Task<bool> DeleteUser(string UserId);
        public Task<User?> GetUserusingUserName(Models.Requests.UsersRequest user);
        public Task<Apistatus> ResetPassword(string emailid);
    }
}
