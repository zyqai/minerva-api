using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using Minerva.DataAccessLayer;
using Minerva.Models.Returns;

namespace MinervaApi.BusinessLayer
{
    public class RequestTemplateBL : IRequestTemplateBL
    {
        IRequestTemplateRepository RequestTemplaterepository;
        IRequestTemplateDetailsRepository requestTemplateDetailsrepository;
        IUserRepository UserRepository;
        public RequestTemplateBL(IRequestTemplateRepository _repository, IUserRepository userRepository, IRequestTemplateDetailsRepository _requestTemplateDetailsrepository)
        {
            RequestTemplaterepository = _repository;
            UserRepository = userRepository;
            requestTemplateDetailsrepository = _requestTemplateDetailsrepository;
        }

        public Task<bool> DeleteRequestTemplate(int RequestTemplateAutoId)
        {
            return RequestTemplaterepository.DeleteRequestTemplate(RequestTemplateAutoId);
        }

        public Task<RequestTemplateResponse?> GetALLRequestTemplates()
        {
            return RequestTemplaterepository.GetALLRequestTemplatesAsync();
        }

        public async Task<RequestTemplate?> GetRequestTemplate(int requestTemplatesId)
        {
            return await RequestTemplaterepository.GetRequestTemplateAsync(requestTemplatesId);
        }

        public async Task<Apistatus> SaveRequestTemplate(RequestTemplateRequestWhithDetails request, string email)
        {
            User? u = await UserRepository.GetuserusingUserNameAsync(email);
            request.TenantId = (int)(u?.TenantId);

            return await RequestTemplaterepository.SaveRequestTemplate(request);
        }

        public async Task<bool> UpdateRequestTemplates(RequestTemplateRequest request)
        {
            User? u = await UserRepository.GetuserusingUserNameAsync(request.email);
            request.tenantId = (int)(u?.TenantId);


            RequestTemplate RequestTemplate = Mapping(request);
            return await RequestTemplaterepository.UpdateRequestTemplate(RequestTemplate);
        }

        private RequestTemplate Mapping(RequestTemplateRequest request)
        {
            RequestTemplate dc = new RequestTemplate();

            dc.requestTemplateId = request.requestTemplateId;

            dc.tenantId = request.tenantId;

            dc.requestTemplateName = request.requestTemplateName;

            dc.requestTemplateDescription = request.requestTemplateDescription;

            dc.remindersAutoId = request.remindersAutoId;

            return dc;
        }
    }
}
