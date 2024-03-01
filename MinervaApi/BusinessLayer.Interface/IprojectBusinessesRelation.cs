using Minerva.Models.Returns;
using MinervaApi.Models.Requests;

namespace MinervaApi.BusinessLayer.Interface
{
    public interface IprojectBusinessesRelation
    {
        public Task<int> Create(projectBusinessesRelationRequest request);
        public Task<bool> DeleteProjectBusinessRelation(int id);
        public Task<ProjectByBusiness> GetProjectByBusiness(int? projectId);
    }
}
