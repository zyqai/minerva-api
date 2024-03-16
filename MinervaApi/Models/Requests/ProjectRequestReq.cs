using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.Models.Returns;

namespace MinervaApi.Models.Requests
{
    public class ProjectRequestReq
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
        public int? ModifiedBy { get; set; }
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

    public class ProjectRequestData
    {
        public string? RequestName { get; set; }
        public string? RequestDescription { get; set; }
        public int? ProjectId { get; set; }
        public int? ReminderId { get; set; }
        public int? TenentId { get; set; }
        public List<RequestSendToData?>? RequestSendTo { get; set; }
        public List<RequestDetailData?>? RequestDetails { get; set; }
    }

    public class RequestSendToData
    {
        public string? SendTo { get; set; }
        public string? SendCC { get; set; }
        public int? Status { get; set; }
    }

    public class RequestDetailData
    {
        public string? Label { get; set; }
        public int? DocumentTypeAutoId { get; set; }
    }

    public class ProjectRequestDetailUpdateData
    {
        public int? ProjectRequestId { get; set; }
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public int? RemindersAutoId { get; set; }
        public string? ProjectRequestName { get; set; }
        public string? ProjectRequestDescription { get; set; }
        public int? ProjectRequestSentId { get; set; }
        public string? SentTo { get; set; }
        public string? SentCC { get; set; }
        public int? StatusAutoId { get; set; }
        public int? ProjectRequestDetailsId { get; set; }
        public string? Label { get; set; }
        public int? DocumentTypeAutoId { get; set; }
    }
    public class ProjectEmailDetails
    {
        public string? token { get; set; }
    }
    public class ProjectEmailRes
    {
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentLabel { get; set; }
        public string? emailid { get; set; }
       
    }
    public class ProjectEmailResponce : Apistatus
    {
        public Project? Project { get; set; }
        public List<ProjectRequestDetails?>? ProjectRequestResponse { get;set;}
        public List<FileType?>? FileFormt { get; set; }
    }
    public class ProjectRequestUrl
    {
        public int? RequestUrlId { get; set; }
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public int? ProjectRequestId { get; set; }
        public string? RequestURL { get; set; }
        public string? Token { get; set; }
        public DateTime?  CreatedOn { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
    public class ProjectRequestDetails
    {
        public int? ProjectRequestDetailsId { get; set; }
        public int? ProjectRequestTemplateId { get; set; }
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public string? Label { get; set; }
        public string? DocumentTypeName { get; set; }
        public string? DocumentTypeDescription { get; set; }
        public string? DocumentClassificationName { get; set; }
        public int? documentTypeAutoId { get; set; }
        public int? documentTypeId { get; set; }
        public int? documentClassificationAutoId { get; set; }
        public int? documentClassificationId { get; set; }
    }
}
