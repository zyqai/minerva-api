using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface ICBRelationRepository
    {
        public Task<bool> Delete(int id);
        public Task<List<CBRelation?>> GelAllAync();
        public Task<CBRelation?> GetAync(int? id);
        public Task<int?>Save(CBRelation? relation);
        public Task<bool> Update(CBRelation relation);
    }
}
