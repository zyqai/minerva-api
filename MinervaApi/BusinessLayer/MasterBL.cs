
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;

namespace MinervaApi.BusinessLayer
{
    public class MasterBL : IMasterBL
    {
        IMasterRepository Repository;
        IUserRepository UserRepository;
        public MasterBL(IMasterRepository master,IUserRepository use) 
        {
            Repository = master;
            UserRepository = use;
        }
        public Task<List<Industrys>> GetindustryAsync()
        {
            return Repository.GetindustrysAsync();
        }

        public Task<List<loanTypes>> GetloanTypesAsync()
        {
            return Repository.GetloanTypesAsync();
        }

        public Task<List<Statuses>> getStatues()
        {
            return Repository.GetStatusAsync();
        }

        public Task<List<Statuses>> getStatues(int projectRequeststatus)
        {
            return Repository.GetStatusAsync(projectRequeststatus);
        }

        public async Task<Apistatus> SaveNotes(Notes request)
        {
            User? user = await UserRepository.GetuserusingUserNameAsync(request.createdByUserId);
            request.createdByUserId = user?.UserId;
            request.tenantId= user?.TenantId;
            return await Repository.SaveNotes(request);
        }
    }
}
