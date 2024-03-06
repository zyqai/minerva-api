namespace MinervaApi.Models
{
    public class ProjectRequestReq
    {
    }
    public class ProjectRequestSentTo
    {
        public int? ProjectRequestSentId { get; set; }
        public int? ProjectRequestTemplateId { get; set; }
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public string? SentTo { get; set; }
        public string? SentCC { get; set; }
        public DateTime? SentOn { get; set; }
        public string? UniqueLink { get; set; }
        public int? StatusAutoId { get; set; }
    }
    public class ProjectRequestDetail
    {
        public int? ProjectRequestDetailsId { get; set; }
        public int? ProjectRequestTemplateId { get; set; }
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public string? Label { get; set; }
        public int? DocumentTypeAutoId { get; set; }
    }
}
