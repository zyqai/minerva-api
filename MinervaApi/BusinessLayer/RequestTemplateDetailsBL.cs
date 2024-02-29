using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.Models.Requests;

namespace Minerva.BusinessLayer
{
    public class RequestTemplateDetailsBL:IRequestTemplateDetailsBL
    {

        IRequestTemplateDetailsRepository RequestTemplateDetailsrepository;
        public RequestTemplateDetailsBL(IRequestTemplateDetailsRepository _repository)
        {
            RequestTemplateDetailsrepository = _repository;
        }

        public Task<bool> DeleteRequestTemplateDetails(int requestTemplateDetailsId)
        {
            return RequestTemplateDetailsrepository.DeleteRequestTemplateDetails(requestTemplateDetailsId);
        }

        public Task<List<RequestTemplateDetails?>> GetALLRequestTemplateDetails()
        {
            return RequestTemplateDetailsrepository.GetALLRequestTemplateDetailssAsync();
        }

        public Task<RequestTemplateDetails?> GetRequestTemplateDetails(int requestTemplateDetailsId)
        {
            return RequestTemplateDetailsrepository.GetRequestTemplateDetailsAsync(requestTemplateDetailsId);
        }

        public Task<bool> SaveRequestTemplateDetails(RequestTemplateDetailsRequest request)
        {
            RequestTemplateDetails RequestTemplateDetails = Mapping(request);
            return RequestTemplateDetailsrepository.SaveRequestTemplateDetails(RequestTemplateDetails);
        }

        public Task<bool> UpdateRequestTemplateDetails(RequestTemplateDetailsRequest request)
        {
            RequestTemplateDetails RequestTemplateDetails = Mapping(request);
            return RequestTemplateDetailsrepository.UpdateRequestTemplateDetails(RequestTemplateDetails);
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
