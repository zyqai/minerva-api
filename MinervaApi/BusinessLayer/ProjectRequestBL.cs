using MinervaApi.BusinessLayer.Interface;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models.Returns;

namespace MinervaApi.BusinessLayer
{
    public class ProjectRequestBL : IProjectRequestBL
    {
        IProjectRequestRepository repository;
        public ProjectRequestBL(IProjectRequestRepository _repository) 
        {
            this.repository = _repository;
        }
        public Task<ProjectRequestDetailsResponse> GetALLAsync(int projectId)
        {
            return repository.GetALLAsyncWithProjectId(projectId);
        }
    }
}
