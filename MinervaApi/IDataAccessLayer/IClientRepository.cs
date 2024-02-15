using Minerva.Models;
using Minerva.Models.Responce;

namespace Minerva.IDataAccessLayer
{
    public interface IClientRepository
    {
        public Task<Client?> GetClientAsync(int? ClientId);
        public Task<List<Client?>> GetALLClientsAsync();
        public Task<int> SaveClient(Client us);
        public Task<bool> UpdateClient(Client us);
        public Task<bool> DeleteClient(int? ClientId);
        public Task<List<ClientPersonas>> GetClientPersonasAsync(int? businessId);
        public Task<List<Client?>> GetAllpeoplesAsynctenant(int tenantId);
    }
}
