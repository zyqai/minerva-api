using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.BusinessLayer.Interface
{
    public interface ITenant
    {
        public Task<Tenant?> GetTenantAsync(int? TenantId);
        public Task<List<Tenant?>> GetALLAsync();
        public Task<int> SaveTenant(TenantRequest t);
        public Task<bool> UpdateTenant(TenantRequest t);
        public Task<bool> DeleteTenant(int TenantId);
    }
}
