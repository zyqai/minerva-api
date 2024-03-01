
using Minerva.Models.Returns;

namespace Minerva.Models
{
    public class DocumentClassification
    {
        public int DocumentClassificationAutoId { get; set; }
        public int DocumentClassificationId { get; set; }
        public int TenantId { get; set; }
        public string? DocumentClassificationName { get; set; }

    }
    public class DocumentClassificationResponse : Apistatus
    {
        public List<DocumentClassification>? DocumentClassifications { get; set; }
    }
}

