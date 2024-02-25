using MinervaApi.Models.Requests;

namespace MinervaApi.IDataAccessLayer
{
    public interface IprojectBusinessesRelationRepository
    {
        public Task<int> CreateProjectBusinessRelation(projectBusinessesRelationRequest request);
    }
}
