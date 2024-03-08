
using MinervaApi.ExternalApi;
using MinervaApi.Models.Returns;

namespace MinervaApi.BusinessLayer.Interface
{
    public interface IProjectRequestBL
    {
        public Task<ProjectRequestDetailsResponse> GetALLAsync(int projectId);
        public Task<ProjectRequestWithDetails> GetALLProjectRequestByIdasync(int projectRequestId);
        public Task<APIStatus> SaveProjectRequestDetails(Models.Requests.ProjectRequestDetail request,string email);
        public Task<APIStatus> UpdateProjectRequestDetails(Models.Requests.ProjectRequestDetail request);

        public Task<APIStatus> SaveProjectRequestSentTo(Models.Requests.ProjectRequestSentTo request, string email);
        public Task<APIStatus> UpdateProjectRequestSentTo(Models.Requests.ProjectRequestSentTo request);
    }
}
