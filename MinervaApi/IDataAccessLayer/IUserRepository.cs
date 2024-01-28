using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IUserRepository
    {
        public Task<User?> GetuserAsync(string ?Userid);
        public Task<User?> GetuserusingUserNameAsync(string ?Username);
        public Task<List<User?>> GetALLAsync();
        public Task<string> SaveUser(User us);
        public Task<bool> UpdateUser(User us);
        public Task<bool> DeleteUser(string UserId);

    }
}
