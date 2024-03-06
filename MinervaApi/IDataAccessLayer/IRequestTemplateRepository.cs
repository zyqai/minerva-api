using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Returns;

namespace Minerva.IDataAccessLayer
{
    public interface IRequestTemplateRepository
    {
        public Task<RequestTemplate?> GetRequestTemplateAsync(int? RequestTemplateId);
        public Task<RequestTemplateResponse?> GetALLRequestTemplatesAsync();
        public Task<Apistatus> SaveRequestTemplate(RequestTemplateRequestWhithDetails dt);
        public Task<bool> UpdateRequestTemplate(RequestTemplate dt);
        public Task<bool> DeleteRequestTemplate(int? RequestTemplateId);


    }
}
