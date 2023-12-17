using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IAdminUserRepository
    {
        public Task<AdminUser?> FindOneAsync(int id);
        public Task<AdminUser?> FindUserPassword(string username, string password);
        
    }
}