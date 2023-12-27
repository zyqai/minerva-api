using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IUserBL
    {
        public Task<User?> GetUser(Models.Requests.UsersRequest user);
        public Task<List<User?>> GetALLUsers();
        public bool SaveUser(Models.Requests.UsersRequest user);
        public bool UpdateUser(Models.Requests.UsersRequest user);
        public bool DeleteUser(string UserId);
    }
}
