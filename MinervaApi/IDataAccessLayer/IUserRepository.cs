using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IUserRepository
    {
        public Task<User?> GetuserAsync(string ?Userid);
        public Task<List<User?>> GetALLAsync();
        public bool SaveUser(User us);
        public bool UpdateUser(User us);
    }
}
