using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.Models;

namespace MinervaApi.IDataAccessLayer
{
    public interface IMasterRepository
    {
        public Task<List<Industrys>> GetindustrysAsync();
        public Task<List<loanTypes>> GetloanTypesAsync();
        public Task<List<Statuses>> GetStatusAsync();

        public Task<Industrys> GetIndustrysByIdAsync(int? id);
        public Task<loanTypes> GetloanTypesByIdAsync(int? id);
        public Task<Statuses> GetStatusByIdAsync(int? id);
        public Task<Apistatus> SaveNotes(Notes request);
    }
}
