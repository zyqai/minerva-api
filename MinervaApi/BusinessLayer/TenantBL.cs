using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;
using MinervaApi.DataAccessLayer;

namespace Minerva.BusinessLayer
{
    public class TenantBL : ITenant
    {
        ITenantRepositiry repositiry;
        public TenantBL(ITenantRepositiry tenant) 
        {
            repositiry = tenant;
        }
        public Task<bool> DeleteTenant(int TnantId)
        {
            return repositiry.DeleteTenant(TnantId);
        }

        public Task<Tenant?> GetTenantAsync(int? TenantId)
        {
            return repositiry.GetTenantAsync(TenantId);
        }

        public Task<bool> SaveTenant(TenantRequest t)
        {
            Tenant ten = Mapping(t);
            return repositiry.SaveTenant(ten);
        }

        private Tenant Mapping(TenantRequest t)
        {
            Tenant tenant = new Tenant 
            {
                TenantId = t.TenantId,
                TenantAddress = t.TenantAddress,
                TenantName = t.TenantName,  
                TenantContactEmail = t.TenantContactEmail,
                TenantContactName = t.TenantContactName,
                TenantDomain = t.TenantDomain,
                TenantLogoPath = t.TenantLogoPath,  
                TenantPhone = t.TenantPhone 
            };
            return tenant;
        }

        public Task<bool> UpdateTenant(TenantRequest t)
        {
            Tenant tr = Mapping(t);
            return repositiry.UpdateTenant(tr);
        }

        public Task<List<Tenant?>> GetALLAsync()
        {
            return repositiry.GetALLAsync();
        }
    }
}
