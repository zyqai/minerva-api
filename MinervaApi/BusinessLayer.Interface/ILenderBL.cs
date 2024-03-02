using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.Models;

namespace MinervaApi.BusinessLayer.Interface
{
    public interface ILenderBL
    {
        public Task<Lender?> GetLender(int LenderId);
        public Task<List<Lender?>> GetALLLenders();
        public Task<int> SaveLender(Models.Requests.LenderRequest Lender, string userid);
        public Task<Apistatus> UpdateLender(Models.Requests.LenderRequest Lender);
        public Task<Apistatus> DeleteLender(int LenderId);
    }
}
