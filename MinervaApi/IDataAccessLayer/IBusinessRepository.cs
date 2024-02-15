using Minerva.Models;
using Minerva.Models.Responce;

namespace Minerva.IDataAccessLayer
{
    public interface IBusinessRepository
    {
        public Task<int?> SaveBusiness(Business bs);
        public Task<Business?> GetBussinessAsync(int ?businesId);
        public Task<List<Business?>> GetAllBussinessAsync();

        public bool UpdateBusiness(Business bs);
        public bool DeleteBusiness(int BusinesId);
        public Task<List<Business?>> GetAllBussinessAsynctenant(int tenantId);
        public Task<List<BusinessPersonas>> GetBussinessPersonasAsync(int? clientId);
    }
}
