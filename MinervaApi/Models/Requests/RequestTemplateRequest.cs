namespace Minerva.Models.Requests
{
    public class RequestTemplateRequest
    {
        public int requestTemplateId { get; set; }
        public int tenantId { get; set; }
        public string? requestTemplateName { get; set; }
        public string? requestTemplateDescription { get; set; }
        public int? remindersAutoId { get; set; }
        public string? email { get; set; }
    }

    public class RequestTemplateRequestWhithDetails
    {
        public int? TenantId { get; set; }
        public string? RequestTemplateName { get; set; }
        public string? RequestTemplateDescription { get; set; }
        public int? RemindersAutoId { get; set; }
        public List<RequestTemplateDetailwithDetails>? RequestTemplateDetails { get; set; }
    }
    public class RequestTemplateDetailwithDetails
    {
        public int? RequestTemplateId { get; set; }
        public string? Label { get; set; }
        public int? DocumentTypeAutoId { get; set; }
    }
}
