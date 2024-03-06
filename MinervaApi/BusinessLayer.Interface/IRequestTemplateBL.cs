using Minerva.Models.Requests;
using Minerva.Models;
using Minerva.Models.Returns;

namespace Minerva.BusinessLayer.Interface
{
    public interface IRequestTemplateBL
    {
        public Task<Apistatus> SaveRequestTemplate(RequestTemplateRequestWhithDetails request,string email);
        public Task<RequestTemplate?> GetRequestTemplate(int RequestTemplateAutoId);
        public Task<RequestTemplateResponse?> GetALLRequestTemplates();
        public Task<bool> UpdateRequestTemplates(RequestTemplateRequest request);
        public Task<bool> DeleteRequestTemplate(int RequestTemplateAutoId);
    }
}
