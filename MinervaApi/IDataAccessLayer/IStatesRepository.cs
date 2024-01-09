using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IStatesRepository
    {
        public Task<States?> GetStateAsync(int? id);
        public Task<List<States?>> GetALLStatesAsync();
        public Task<bool> SaveState(States us);
        public Task<bool> UpdateState(States us);
        public Task<bool> DeleteState(int? id);
    }
}
