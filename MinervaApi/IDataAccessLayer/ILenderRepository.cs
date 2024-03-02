using Minerva.Models.Returns;
using MinervaApi.Models;

namespace MinervaApi.IDataAccessLayer
{
    public interface ILenderRepository
    {
        public Task<Lender?> GetLender(int lenderId);
        public Task<int> SaveLender(Lender req);
        public Task<List<Lender?>> GetALLLenders();
        public Task<Apistatus> UpdateLendet(Lender lender);
        public Task<Apistatus> DeleteLender(int lenderId);
    }
}
