using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IClientRepository
    {
        public Task<Client?> GetClientAsync(int? ClientId);
        public Task<List<Client?>> GetALLClientsAsync();
        public Task<bool> SaveClient(Client us);
        public Task<bool> UpdateClient(Client us);
        public Task<bool> DeleteClient(int? ClientId);
    }
}
