using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IDocumentClassificationRepository
    {
        public Task<DocumentClassification?> GetDocumentClassificationAsync(int? DocumentClassificationAutoId);
        public Task<List<DocumentClassification?>> GetALLDocumentClassificationsAsync();
        public Task<bool> SaveDocumentClassification(DocumentClassification dc);
        public Task<bool> UpdateDocumentClassification(DocumentClassification dc);
        public Task<bool> DeleteDocumentClassification(int? DocumentClassificationAutoId);
    }
}
