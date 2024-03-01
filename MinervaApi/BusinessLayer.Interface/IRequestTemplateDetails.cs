using Minerva.Models.Requests;
using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IRequestTemplateDetailsBL
    {
        public Task<int> SaveRequestTemplateDetails(RequestTemplateDetailsRequest request);
        public Task<RequestTemplateDetails?> GetRequestTemplateDetails(int requestTemplateDetailsId);
        public Task<RequestTemplateDetailsResponse?> GetALLRequestTemplateDetails();
        public Task<bool> UpdateRequestTemplateDetails(RequestTemplateDetailsRequest request);
        public Task<bool> DeleteRequestTemplateDetails(int requestTemplateDetailsId);
    }
}
