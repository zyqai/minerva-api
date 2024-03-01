using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using Minerva.DataAccessLayer;

namespace MinervaApi.BusinessLayer
{
         public class RequestTemplateBL : IRequestTemplateBL
        {
            IRequestTemplateRepository RequestTemplaterepository;
        IUserRepository UserRepository;
        public RequestTemplateBL(IRequestTemplateRepository _repository, IUserRepository userRepository)
            {
                RequestTemplaterepository = _repository;
            UserRepository = userRepository;
        }

            public Task<bool> DeleteRequestTemplates(int RequestTemplateAutoId)
            {
                return RequestTemplaterepository.DeleteRequestTemplate(RequestTemplateAutoId);
            }

            public Task<RequestTemplateResponse?> GetALLRequestTemplates()
            {
                return RequestTemplaterepository.GetALLRequestTemplatesAsync();
            }

            public Task<RequestTemplate?> GetRequestTemplate(int requestYemplatesId)
            {
                return RequestTemplaterepository.GetRequestTemplateAsync(requestYemplatesId);
            }

            public async Task<int> SaveRequestTemplate(RequestTemplateRequest request)
            {
                User? u = await UserRepository.GetuserusingUserNameAsync(request.email);
                request.tenantId = (int)(u?.TenantId);

                RequestTemplate RequestTemplate = Mapping(request);
                return await RequestTemplaterepository.SaveRequestTemplate(RequestTemplate);
            }

            public async Task<bool> UpdateRequestTemplates(RequestTemplateRequest request)
            {
            User? u = await UserRepository.GetuserusingUserNameAsync(request.email);
            request.tenantId = (int)(u?.TenantId);


            RequestTemplate RequestTemplate = Mapping(request);
                return await RequestTemplaterepository.UpdateRequestTemplate(RequestTemplate);
            }

            Task<bool> IRequestTemplateBL.DeleteRequestTemplate(int RequestTemplateAutoId)
            {
                throw new NotImplementedException();
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
