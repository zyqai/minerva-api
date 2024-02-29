using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;

namespace MinervaApi.BusinessLayer
{
         public class RequestTemplateBL : IRequestTemplateBL
        {
            IRequestTemplateRepository RequestTemplaterepository;
            public RequestTemplateBL(IRequestTemplateRepository _repository)
            {
                RequestTemplaterepository = _repository;
            }

            public Task<bool> DeleteRequestTemplates(int RequestTemplateAutoId)
            {
                return RequestTemplaterepository.DeleteRequestTemplate(RequestTemplateAutoId);
            }

            public Task<List<RequestTemplate?>> GetALLRequestTemplates()
            {
                return RequestTemplaterepository.GetALLRequestTemplatesAsync();
            }

            public Task<RequestTemplate?> GetRequestTemplate(int requestYemplatesId)
            {
                return RequestTemplaterepository.GetRequestTemplateAsync(requestYemplatesId);
            }

            public Task<int> SaveRequestTemplate(RequestTemplateRequest request)
            {
                RequestTemplate RequestTemplate = Mapping(request);
                return RequestTemplaterepository.SaveRequestTemplate(RequestTemplate);
            }

            public Task<bool> UpdateRequestTemplates(RequestTemplateRequest request)
            {
                RequestTemplate RequestTemplate = Mapping(request);
                return RequestTemplaterepository.UpdateRequestTemplate(RequestTemplate);
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
