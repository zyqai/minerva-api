
using MinervaApi.Models;

namespace MinervaApi.BusinessLayer.Interface
{
    public interface IMasterBL
    {
        public Task<List<Industrys>> GetindustryAsync();
    }
}
