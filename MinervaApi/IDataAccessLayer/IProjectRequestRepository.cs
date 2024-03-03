using MinervaApi.Models.Returns;

namespace MinervaApi.IDataAccessLayer
{
    public interface IProjectRequestRepository
    {
        public Task<ProjectRequestDetailsResponse> GetALLAsyncWithProjectId(int projectId);
    }
}
