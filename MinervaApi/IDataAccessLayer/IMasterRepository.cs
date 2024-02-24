using MinervaApi.Models;

namespace MinervaApi.IDataAccessLayer
{
    public interface IMasterRepository
    {
        public Task<List<Industrys>> GetindustrysAsync();
    }
}
