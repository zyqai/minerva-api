using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IRequestTemplateRepository
    {
        public Task<RequestTemplate?> GetRequestTemplateAsync(int? RequestTemplateId);
        public Task<List<RequestTemplate?>> GetALLRequestTemplatesAsync();
        public Task<int> SaveRequestTemplate(RequestTemplate dt);
        public Task<bool> UpdateRequestTemplate(RequestTemplate dt);
        public Task<bool> DeleteRequestTemplate(int? RequestTemplateId);


    }
}
