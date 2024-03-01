using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IDocumentTypeRepository
    {
        public Task<DocumentType?> GetDocumentTypeAsync(int? DocumentTypeAutoId);
        public Task<DocumentTypeResponse?> GetALLDocumentTypesAsync();
        public Task<bool> SaveDocumentType(DocumentType dt);
        public Task<bool> UpdateDocumentType(DocumentType dt);
        public Task<bool> DeleteDocumentType(int? DocumentTypeAutoId);
    }
}
