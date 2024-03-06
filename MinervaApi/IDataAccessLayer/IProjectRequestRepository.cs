﻿using MinervaApi.ExternalApi;
using MinervaApi.Models.Returns;

namespace MinervaApi.IDataAccessLayer
{
    public interface IProjectRequestRepository
    {
        public Task<ProjectRequestDetailsResponse> GetALLAsyncWithProjectId(int projectId);
        public Task<ProjectRequest?> GetAllProjectRequestById(int projectRequestId);
        
        public Task<List<ProjectRequestSentTo>?> GetAllProjectRequestSentToByRequestId(int projectRequestId);
        public Task<List<ProjectRequestDetail>?> GetAllProjectRequestDetailsByRequestId(int projectRequestId);
        public Task<APIStatus> SaveProjectRequestDetails(Models.ProjectRequestDetail prd);
    }
}
