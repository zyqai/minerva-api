namespace Minerva.Models
{
    public class RequestTemplateDetailsRequest
    {
        public int RequestTemplateDetailsId { get; set; }
        public int? RequestTemplateId { get; set; }
        public int? TenantId { get; set; }
        public string? Label { get; set; }
        public int? DocumentTypeAutoId { get; set; }
        public string? email { get; set; }

    }
}