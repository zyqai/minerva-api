
using Minerva.Models;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;

namespace MinervaApi.BusinessLayer
{
    public class MasterBL : IMasterBL
    {
        IMasterRepository Repository;
        public MasterBL(IMasterRepository master) 
        {
            Repository = master;
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
    }
}
