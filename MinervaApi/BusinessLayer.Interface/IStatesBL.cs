using Minerva.Models.Requests;
using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IStatesBL
    {
        public Task<bool> SaveState(StatesRequest request);
        public Task<States?> Getstates(int Id);
        public Task<List<States?>> GetALLstates();
        public Task<bool> UpdateStates(StatesRequest request);
        public Task<bool> DeleteStates(int id);
    }
}
