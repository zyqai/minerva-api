using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.Models.Requests;
using Minerva.DataAccessLayer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minerva.BusinessLayer
{
    public class RequestTemplateDetailsBL:IRequestTemplateDetailsBL
    {

        IRequestTemplateDetailsRepository RequestTemplateDetailsrepository;
        IUserRepository UserRepository;

        public RequestTemplateDetailsBL(IRequestTemplateDetailsRepository _repository, IUserRepository userRepository)
        {
            RequestTemplateDetailsrepository = _repository;
            UserRepository = userRepository;
        }

        public Task<bool> DeleteRequestTemplateDetails(int requestTemplateDetailsId)
        {
            return RequestTemplateDetailsrepository.DeleteRequestTemplateDetails(requestTemplateDetailsId);
        }

        public Task<RequestTemplateDetailsResponse?> GetALLRequestTemplateDetails()
        {
            return RequestTemplateDetailsrepository.GetALLRequestTemplateDetailssAsync();
        }

        public Task<RequestTemplateDetails?> GetRequestTemplateDetails(int requestTemplateDetailsId)
        {
            return RequestTemplateDetailsrepository.GetRequestTemplateDetailsAsync(requestTemplateDetailsId);
        }

        public async Task<int> SaveRequestTemplateDetails(RequestTemplateDetailsRequest request)
        {
            User? u = await UserRepository.GetuserusingUserNameAsync(request.email);
            request.TenantId = u?.TenantId;

            RequestTemplateDetails RequestTemplateDetails = Mapping(request);
            return await RequestTemplateDetailsrepository.SaveRequestTemplateDetails(RequestTemplateDetails);
        }

        public async Task<bool> UpdateRequestTemplateDetails(RequestTemplateDetailsRequest request)
        {
            User? u = await UserRepository.GetuserusingUserNameAsync(request.email);
            request.TenantId = u?.TenantId;

            RequestTemplateDetails RequestTemplateDetails =  Mapping(request);
            return await RequestTemplateDetailsrepository.UpdateRequestTemplateDetails(RequestTemplateDetails);
        }

        private RequestTemplateDetails Mapping(RequestTemplateDetailsRequest request)
        {
            RequestTemplateDetails dc = new RequestTemplateDetails();

            dc.RequestTemplateDetailsId = request.RequestTemplateDetailsId;

            dc.RequestTemplateId = request.RequestTemplateId;

            dc.TenantId = request.TenantId;

            dc.Label = request.Label;

            dc.DocumentTypeAutoId = request.DocumentTypeAutoId;

            return dc;
        }
    }
}
