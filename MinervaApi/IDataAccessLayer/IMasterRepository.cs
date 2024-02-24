using Minerva.Models;
using MinervaApi.Models;

namespace MinervaApi.IDataAccessLayer
{
    public interface IMasterRepository
    {
        public Task<List<Industrys>> GetindustrysAsync();
        public Task<List<loanTypes>> GetloanTypesAsync();
        public Task<List<Statuses>> GetStatusAsync();
    }
}
