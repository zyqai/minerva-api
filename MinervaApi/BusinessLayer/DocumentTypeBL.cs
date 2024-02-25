using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.Models.Requests;

namespace Minerva.BusinessLayer
{
    public class DocumentTypeBL:IDocumentTypeBL
    {
        IDocumentTypeRepository DocumentTyperepository;
        public DocumentTypeBL(IDocumentTypeRepository _repository)
        {
            DocumentTyperepository = _repository;
        }

        public Task<bool> DeleteDocumentTypes(int DocumentTypeAutoId)
        {
            return DocumentTyperepository.DeleteDocumentType(DocumentTypeAutoId);
        }

        public Task<List<DocumentType?>> GetALLDocumentTypes()
        {
            return DocumentTyperepository.GetALLDocumentTypesAsync();
        }

        public Task<DocumentType?> GetDocumentType(int DocumentTypeAutoId)
        {
            return DocumentTyperepository.GetDocumentTypeAsync(DocumentTypeAutoId);
        }

        public Task<bool> SaveDocumentType(DocumentTypeRequest request)
        {
            DocumentType DocumentType = Mapping(request);
            return DocumentTyperepository.SaveDocumentType(DocumentType);
        }

        public Task<bool> UpdateDocumentTypes(DocumentTypeRequest request)
        {
            DocumentType DocumentType = Mapping(request);
            return DocumentTyperepository.UpdateDocumentType(DocumentType);
        }

        Task<bool> IDocumentTypeBL.DeleteDocumentType(int DocumentTypeAutoId)
        {
            throw new NotImplementedException();
        }

        private DocumentType Mapping(DocumentTypeRequest request)
        {
            DocumentType dc = new DocumentType();

            dc.DocumentTypeAutoId = request.DocumentTypeAutoId;

            dc.DocumentTypeId = request.DocumentTypeId;

            dc.TenantId = request.TenantId;

            dc.DocumentTypeName = request.DocumentTypeName;

            dc.DocumentTypeDescription = request.DocumentTypeDescription;

            dc.DocumentClassificationId = request.DocumentClassificationId;

            dc.TemplateFilePath = request.TemplateFilePath;


            return dc;
        }
    }
}
