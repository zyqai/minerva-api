
using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.Models;

namespace MinervaApi.BusinessLayer.Interface
{
    public interface IMasterBL
    {
        public Task<List<Industrys>> GetindustryAsync();
        public Task<List<loanTypes>> GetloanTypesAsync();
        public Task<List<Statuses>> getStatues();
        public Task<List<Statuses>> getStatues(int projectRequeststatus);
        public Task<Apistatus> SaveNotes(Notes request);
    }
}
