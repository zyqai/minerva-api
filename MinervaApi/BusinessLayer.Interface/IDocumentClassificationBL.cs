using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.Models.Requests;

namespace Minerva.BusinessLayer.Interface
{
    public interface IDocumentClassificationBL
    {
        public Task<bool> SaveDocumentClassification(DocumentClassificationRequest request);
        public Task<DocumentClassification?> GetDocumentClassification(int DocumentClassificationAutoId);
        public Task<List<DocumentClassification?>> GetALLDocumentClassifications();
        public Task<bool> UpdateDocumentClassifications(DocumentClassificationRequest request);
        public Task<bool> DeleteDocumentClassification(int DocumentClassificationAutoId);
    }
}
