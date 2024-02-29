using Minerva.Models.Requests;
using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IRequestTemplateBL
    {
        public Task<int> SaveRequestTemplate(RequestTemplateRequest request);
        public Task<RequestTemplate?> GetRequestTemplate(int RequestTemplateAutoId);
        public Task<List<RequestTemplate?>> GetALLRequestTemplates();
        public Task<bool> UpdateRequestTemplates(RequestTemplateRequest request);
        public Task<bool> DeleteRequestTemplate(int RequestTemplateAutoId);
    }
}
