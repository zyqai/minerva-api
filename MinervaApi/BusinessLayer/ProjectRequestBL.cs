using MinervaApi.BusinessLayer.Interface;
using MinervaApi.ExternalApi;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models.Returns;
using System.Data;

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

        public async Task<ProjectRequestWithDetails> GetALLProjectRequestByIdasync(int projectRequestId)
        {
            ProjectRequestWithDetails pr = new ProjectRequestWithDetails();
            pr.projectRequest=await repository.GetAllProjectRequestById(projectRequestId);
            pr.ProjectRequestSentList = await repository.GetAllProjectRequestSentToByRequestId(projectRequestId);
            pr.ProjectRequestDetailList = await repository.GetAllProjectRequestDetailsByRequestId(projectRequestId);
            if (pr.projectRequest != null)
            {
                pr.code = "200";
                pr.message = "response is available ";
            }
            else
            {
                pr.code = "204";
                pr.message = "response is not available ";


            }
            return pr;
        }

        public Task<APIStatus> SaveProjectRequestDetails(Models.Requests.ProjectRequestDetail request)
        {
            Models.ProjectRequestDetail prd = new Models.ProjectRequestDetail();
            prd = MappingRequest(request);
            return repository.SaveProjectRequestDetails(prd);
        }

        private Models.ProjectRequestDetail MappingRequest(Models.Requests.ProjectRequestDetail request)
        {
            Models.ProjectRequestDetail prd = new Models.ProjectRequestDetail();
            prd.ProjectRequestDetailsId = request.ProjectRequestDetailsId;
            prd.ProjectRequestTemplateId   = request.ProjectRequestTemplateId;
            prd.ProjectId = request.ProjectId;
            prd.Label = request.Label;
            prd.DocumentTypeAutoId = request.DocumentTypeAutoId;    
            return prd;
        }
    }
}
