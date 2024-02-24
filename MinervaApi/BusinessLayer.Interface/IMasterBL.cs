
using Minerva.Models;
using MinervaApi.Models;

namespace MinervaApi.BusinessLayer.Interface
{
    public interface IMasterBL
    {
        public Task<List<Industrys>> GetindustryAsync();
        public Task<List<loanTypes>> GetloanTypesAsync();
        public Task<List<Statuses>> getStatues();
    }
}
