using Minerva.Models.Returns;
using MinervaApi.Models.Requests;

namespace MinervaApi.IDataAccessLayer
{
    public interface IprojectBusinessesRelationRepository
    {
        public Task<int> CreateProjectBusinessRelation(projectBusinessesRelationRequest request);
        public Task<List<BusinessesByProject>> GetProjectByBusinessRelation(int? projectId);
        public Task<List<ResponceprojectBusinessesRelation?>?> GetBusinessByProjectid(int? projectId);
        public Task<bool> DeleteProjectBusinessRelation(int projectBusinessId);
    }
}
