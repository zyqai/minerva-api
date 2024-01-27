using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IClientBL
    {
        public Task<Client?> GetClient(int ClientId);
        public Task<List<Client?>> GetALLClients();
        public Task<int> SaveClient(Models.Requests.ClientRequest Client);
        public Task<bool> UpdateClient(Models.Requests.ClientRequest Client);
        public Task<bool> DeleteClient(int ClientId);
    }
}
