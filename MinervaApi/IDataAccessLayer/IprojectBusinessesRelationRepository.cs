using Minerva.Models.Returns;
using MinervaApi.Models.Requests;

namespace MinervaApi.IDataAccessLayer
{
    public interface IprojectBusinessesRelationRepository
    {
        public Task<int> CreateProjectBusinessRelation(projectBusinessesRelationRequest request);
        public Task<List<BusinessesByProject>> GetProjectByBusinessRelation(int? projectId);
    }
}
