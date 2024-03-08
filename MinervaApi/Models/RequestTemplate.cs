using Minerva.Models.Returns;

namespace Minerva.Models
{
    public class RequestTemplate
    {
        public int requestTemplateId { get; set; }
        public int tenantId { get; set; }
        public string? requestTemplateName { get; set; }
        public string? requestTemplateDescription { get; set; }
        public int? remindersAutoId { get; set; }
        public List<RequestTemplateDetails>? requestTemplateDetails { get; set; }
    }
    public class RequestTemplateResponse : Apistatus
    {
        public List<RequestTemplate>? RequestTemplates { get; set; }
    }

}
