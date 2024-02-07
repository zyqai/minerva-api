using Minerva.Models;
using Minerva.Models.Responce;

namespace Minerva.IDataAccessLayer
{
    public interface ITenantRepositiry
    {
        public Task<Tenant?> GetTenantAsync(int? TenantId);
        public Task<List<Tenant?>> GetALLAsync();
        public Task<int> SaveTenant(Tenant t);
        public Task<bool> UpdateTenant(Tenant t);
        public Task<bool> DeleteTenant(int TenantId);
        //public Task<TenantBusiness> BusinessesForTenant(int tenantId);
        //public Task<PeopleBusiness> PeoplesForTenant(int tenantId);
        //public Task<TenantUsers> UsersForTenant(int tenantId);
    }
}
