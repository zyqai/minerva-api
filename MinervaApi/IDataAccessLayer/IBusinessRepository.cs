using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IBusinessRepository
    {
        public int SaveBusiness(Business bs);
        public Task<Business?> GetBussinessAsync(int ?businesId);
        public Task<List<Business?>> GetAllBussinessAsync();

        public bool UpdateBusiness(Business bs);
        public bool DeleteBusiness(int BusinesId);
        public Task<List<Business?>> GetAllBussinessAsynctenant(int tenantId);
    }
}
