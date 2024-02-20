using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Responce;
using MinervaApi.DataAccessLayer;
using System.Text;

namespace Minerva.BusinessLayer
{
    public class TenantBL : ITenant
    {
        ITenantRepositiry repositiry;
        IUserRepository userRepository;
        IBusinessRepository businessRepository;
        IClientRepository clientRepository;
        public TenantBL(ITenantRepositiry tenant, IUserRepository _user, IBusinessRepository _businessRepository, IClientRepository _clientRepository)
        {
            repositiry = tenant;
            userRepository = _user;
            businessRepository = _businessRepository;
            clientRepository = _clientRepository;
        }
        public Task<bool> DeleteTenant(int TnantId)
        {
            return repositiry.DeleteTenant(TnantId);
        }

        public Task<Tenant?> GetTenantAsync(int? TenantId)
        {
            return repositiry.GetTenantAsync(TenantId);
        }

        public async Task<int> SaveTenant(TenantRequest t)
        {
            User ?user = await userRepository.GetuserusingUserNameAsync(t.CreatedBY);
            t.CreatedBY = user?.UserId;
            Tenant ten = Mapping(t);
            return await repositiry.SaveTenant(ten);
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
                TenantPhone = t.TenantPhone,
                TenantAddress1 = t.TenantAddress1,
                stateid = t.stateid,
                City = t.City,
                PostalCode = t.PostalCode,
                UpdatedBY=t.UpdatedBY,
                CreatedBY=t.CreatedBY,
                tenantDescription=t.tenantDescription,
            };
            return tenant;
        }

        public async Task<bool> UpdateTenant(TenantRequest t)
        {
            User? user = await userRepository.GetuserusingUserNameAsync(t.UpdatedBY);
            t.UpdatedBY = user?.UserId;
            Tenant tr = Mapping(t);
            return await repositiry.UpdateTenant(tr);
        }

        public Task<List<Tenant?>> GetALLAsync()
        {
            return repositiry.GetALLAsync();
        }

        public async Task<TenantBusiness> BusinessesForTenant(int tenantId)
        {
            TenantBusiness tenantbus = new TenantBusiness();
            tenantbus.tenant = await repositiry.GetTenantAsync(tenantId);
            if (tenantbus.tenant!=null)
            {
                tenantbus.business = new List<Business?>();
                tenantbus.business = await businessRepository.GetAllBussinessAsynctenant(tenantId);
            }
            return tenantbus;
        }
        public async Task<PeopleBusiness> PeoplesForTenant(int tenantId)
        {
            PeopleBusiness peoples = new PeopleBusiness();
            peoples.tenant = await repositiry.GetTenantAsync(tenantId);
            if (peoples.tenant != null)
            {
                peoples.peoples = new List<Client?> ();
                peoples.peoples = await clientRepository.GetAllpeoplesAsynctenant(tenantId);
            }
            return peoples;
        }
        public async Task<TenantUsers> UsersForTenant(int tenantId)
        {
            TenantUsers tenantUsers = new TenantUsers();
            tenantUsers.tenant = await repositiry.GetTenantAsync(tenantId);
            if (tenantUsers.tenant != null)
            {
                tenantUsers.users = new List<User?>();
                tenantUsers.users = await userRepository.GetTenantUserList(tenantId);
            }
            return tenantUsers;
        }
    }
}
