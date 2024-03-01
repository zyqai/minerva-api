using Minerva.Models;

namespace Minerva.IDataAccessLayer
{

    public interface IRequestTemplateDetailsRepository
    {
        public Task<RequestTemplateDetails?> GetRequestTemplateDetailsAsync(int? requestTemplateDetailsId);
        public Task<RequestTemplateDetailsResponse?> GetALLRequestTemplateDetailssAsync();
        public Task<int> SaveRequestTemplateDetails(RequestTemplateDetails rt);
        public Task<bool> UpdateRequestTemplateDetails(RequestTemplateDetails rt);
        public Task<bool> DeleteRequestTemplateDetails(int? requestTemplateDetailsId);
    }

}
