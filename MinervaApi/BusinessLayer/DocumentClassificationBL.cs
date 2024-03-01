using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.Models.Requests;

namespace Minerva.BusinessLayer
{
    public class DocumentClassificationBL: IDocumentClassificationBL
    {
        IDocumentClassificationRepository DocumentClassificationrepository;
        public DocumentClassificationBL(IDocumentClassificationRepository _repository)
        {
            DocumentClassificationrepository = _repository;
        }

        public Task<bool> DeleteDocumentClassifications(int DocumentClassificationAutoId)
        {
            return DocumentClassificationrepository.DeleteDocumentClassification(DocumentClassificationAutoId);
        }

        public Task<DocumentClassificationResponse?> GetALLDocumentClassifications()
        {
            return DocumentClassificationrepository.GetALLDocumentClassificationsAsync();
        }

        public Task<DocumentClassification?> GetDocumentClassification(int DocumentClassificationAutoId)
        {
            return DocumentClassificationrepository.GetDocumentClassificationAsync(DocumentClassificationAutoId);
        }

        public Task<bool> SaveDocumentClassification(DocumentClassificationRequest request)
        {
            DocumentClassification DocumentClassification = Mapping(request);
            return DocumentClassificationrepository.SaveDocumentClassification(DocumentClassification);
        }

        public Task<bool> UpdateDocumentClassifications(DocumentClassificationRequest request)
        {
            DocumentClassification DocumentClassification = Mapping(request);
            return DocumentClassificationrepository.UpdateDocumentClassification(DocumentClassification);
        }

        Task<bool> IDocumentClassificationBL.DeleteDocumentClassification(int DocumentClassificationAutoId)
        {
            throw new NotImplementedException();
        }

        private DocumentClassification Mapping(DocumentClassificationRequest request)
        {
            DocumentClassification dc = new DocumentClassification();

            dc.DocumentClassificationAutoId = request.DocumentClassificationAutoId;

            dc.DocumentClassificationId = request.DocumentClassificationId;

            dc.TenantId = request.TenantId;

            dc.DocumentClassificationName = request.DocumentClassificationName;

            return dc;
        }

    }

}
