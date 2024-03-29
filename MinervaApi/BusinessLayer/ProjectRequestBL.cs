﻿using Minerva.IDataAccessLayer;
using Minerva.Models;
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
        IUserRepository userRepository;
        public ProjectRequestBL(IProjectRequestRepository _repository,IUserRepository _user) 
        {
            this.repository = _repository;
            userRepository = _user;
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

        public async Task<APIStatus> SaveProjectRequestDetails(Models.Requests.ProjectRequestDetail request, string email)
        {
            User ?user=await userRepository.GetuserusingUserNameAsync(email);
            Models.ProjectRequestDetail prd = new Models.ProjectRequestDetail();
            prd = MappingRequest(request);
            prd.TenantId = user?.TenantId;    
            return await repository.SaveProjectRequestDetails(prd);
        }

        public Task<APIStatus> UpdateProjectRequestDetails(Models.Requests.ProjectRequestDetail request)
        {
            Models.ProjectRequestDetail prd = new Models.ProjectRequestDetail();
            prd = MappingRequest(request);
            return repository.UpdateProjectRequestDetails(prd);
        }

        private Models.ProjectRequestDetail MappingRequest(Models.Requests.ProjectRequestDetail request)
        {
            Models.ProjectRequestDetail prd = new Models.ProjectRequestDetail();
            prd.ProjectRequestDetailsId = request.ProjectRequestDetailsId;
            prd.ProjectRequestTemplateId = request.ProjectRequestTemplateId;
            prd.ProjectId = request.ProjectId;
            prd.Label = request.Label;
            prd.DocumentTypeAutoId = request.DocumentTypeAutoId;
            return prd;
        }

        public async Task<APIStatus> SaveProjectRequestSentTo(Models.Requests.ProjectRequestSentTo request, string email)
        {
            User? user = await userRepository.GetuserusingUserNameAsync(email);
            Models.ProjectRequestSentTo prst = new Models.ProjectRequestSentTo();
            prst = MappingSenttoRequest(request);
            prst.TenantId = user?.TenantId;
            return await repository.SaveProjectRequestSentTo(prst);
        }

        public Task<APIStatus> UpdateProjectRequestSentTo(Models.Requests.ProjectRequestSentTo request)
        {
            Models.ProjectRequestSentTo prst = new Models.ProjectRequestSentTo();
            prst = MappingSenttoRequest(request);
            return repository.UpdateProjectRequestSentTo(prst);
        }

        private Models.ProjectRequestSentTo MappingSenttoRequest(Models.Requests.ProjectRequestSentTo request)
        {
            Models.ProjectRequestSentTo sentTo = new Models.ProjectRequestSentTo();
            sentTo.ProjectRequestSentId = request.ProjectRequestSentId;
            sentTo.ProjectRequestTemplateId = request.ProjectRequestTemplateId;
            sentTo.ProjectId = request.ProjectId;
            sentTo.SentTo = request.SentTo;
            sentTo.SentCC = request.SentCC;
            sentTo.SentOn = request.SentOn;
            sentTo.UniqueLink = request.UniqueLink;
            sentTo.StatusAutoId = request.StatusAutoId;
            return sentTo;
        }
    }
}
