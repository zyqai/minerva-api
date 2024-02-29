namespace Minerva.Models
{

    public class RequestTemplateDetails
    {
        public int RequestTemplateDetailsId { get; set; }
        public int? RequestTemplateId { get; set; }
        public int? TenantId { get; set; }
        public string? Label { get; set; }
        public int? DocumentTypeAutoId { get; set; }

    }
}
