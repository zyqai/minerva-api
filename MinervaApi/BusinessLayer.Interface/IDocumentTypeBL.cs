using Minerva.Models.Requests;
using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IDocumentTypeBL
    {
        public Task<bool> SaveDocumentType(DocumentTypeRequest request);
        public Task<DocumentType?> GetDocumentType(int DocumentTypeAutoId);
        public Task<DocumentTypeResponse?> GetALLDocumentTypes();
        public Task<bool> UpdateDocumentTypes(DocumentTypeRequest request);
        public Task<bool> DeleteDocumentType(int DocumentTypeAutoId);

    }
}
