namespace Minerva.Models
{
    public class RequestTemplate
    {
        public int requestTemplateId { get; set; }
        public int tenantId { get; set; }
        public string? requestTemplateName { get; set; }
        public string? requestTemplateDescription { get; set; }
        public int? remindersAutoId { get; set; }
    }
}
