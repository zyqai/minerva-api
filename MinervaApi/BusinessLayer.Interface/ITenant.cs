using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Responce;

namespace Minerva.BusinessLayer.Interface
{
    public interface ITenant
    {
        public Task<Tenant?> GetTenantAsync(int? TenantId);
        public Task<List<Tenant?>> GetALLAsync();
        public Task<int> SaveTenant(TenantRequest t);
        public Task<bool> UpdateTenant(TenantRequest t);
        public Task<bool> DeleteTenant(int TenantId);
        public Task<TenantBusiness> BusinessesForTenant(int tenantId);
        public Task<PeopleBusiness> PeoplesForTenant(int tenantId);
        public Task<TenantUsers> UsersForTenant(int tenantId);
    }
}
