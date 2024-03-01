using Minerva.Models.Returns;

namespace Minerva.Models
{
    public class DocumentType
    {
        public int DocumentTypeAutoId { get; set; }
        public int DocumentTypeId { get; set; }
        public int TenantId { get; set; }
        public string? DocumentTypeName { get; set; }
        public string? DocumentTypeDescription { get; set; }
        public int? DocumentClassificationId { get; set; }
        public string? TemplateFilePath { get; set; }

    }

    public class DocumentTypeResponse : Apistatus
    {

        public List<DocumentType>? DocumentTypes { get; set; }
    }
}
