using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.BusinessLayer.Interface
{
    public interface IBusinessBL
    {
        public bool SaveBusines(BusinessRequest request);
        public Task<Business?> GetBusiness(int BusinesId);
        public Task<List<Business?>> GetALLBusiness();
        public bool UpdateBusiness(BusinessRequest request);
        public bool DeleteBusiness(int businessId);
    }
}
