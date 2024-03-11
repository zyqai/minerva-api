using Minerva.Models.Requests;
using Minerva.Models.Returns;
using MinervaApi.Models.Requests;

namespace MinervaApi.Models.Returns
{
    public class ProjectRequestResponse
    {
        public int? projectRequestId { get; set; }
        public int? projectId { get; set; }
        public int? tenantId { get; set; }
        public string? projectRequestName { get; set; }
        public string? label { get; set; }
        public string? sentTo { get; set; }
        public string? sentcc { get; set; }
        public string? documentTypeName { get; set; }
        public string? statusName { get; set; }
        public string? documentClassificationName { get; set;}
        public DateTime? createdOn { get; set; }
        public DateTime? modifiedOn { get; set; }
    }
    public class ProjectRequestDetailsResponse:Apistatus
    { 
        public List<ProjectRequestResponse?>? ProjectRequest { get; set; }
    }
    public class ProjectRequest
    {
        public int? ProjectRequestId { get; set; }
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public int? RemindersAutoId { get; set; }
        public string? ProjectRequestName { get; set; }
        public string? ProjectRequestDescription { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
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
        public string? statusName { get; set; }
        public string? statusDescription { get; set; }
    }
    public class ProjectRequestDetail
    {
        public int? ProjectRequestDetailsId { get; set; }
        public int? ProjectRequestTemplateId { get; set; }
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public string? Label { get; set; }
        public int? DocumentTypeAutoId { get; set; }
        public string? DocumentTypeName { get; set; }
        public string? DocumentTypeDescription { get; set; }
        public string? TemplateFilePath { get; set; }
    }
    public class ProjectRequestWithDetails : Apistatus
    {
        public ProjectRequest? projectRequest { get; set; }
        public List<ProjectRequestSentTo>? ProjectRequestSentList { get;set;}
        public List<ProjectRequestDetail>? ProjectRequestDetailList { get; set;}
    }
}
