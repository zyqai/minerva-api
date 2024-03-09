using MinervaApi.Models.Requests;

namespace Minerva.Models.Requests
{
    public class ProjectRequest
    {
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public int? IndustryId { get; set; }
        public string? Amount { get; set; }
        public string? Purpose { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime?  CreatedDateTime { get; set; }
        public string? AssignedToUserId { get; set; }
        public string? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int? LoanTypeAutoId { get; set; }
        public int? StatusAutoId { get; set; }
        public string? ProjectFilesPath { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? DesiredClosedDate { get; set; }
        public string? Notes { get; set; }
        public int? PrimaryBorrower { get; set; } // People ID
        public int? PrimaryBusiness { get; set; } // BusinessId

    }
    public class ProjectwithDetailsRequest
    {
        public int? TenantId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public int? IndustryId { get; set; }
        public string? Amount { get; set; }
        public string? Purpose { get; set; }
        public string? AssignedToUserId { get; set; }
        public int? LoanTypeAutoId { get; set; }
        public int? StatusAutoId { get; set; }
        public string? ProjectFilesPath { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? DesiredClosedDate { get; set; }
        public string? Notes { get; set; }
        public int? PrimaryBorrower { get; set; } // People ID
        public int? PrimaryBusiness { get; set; } // BusinessId
        public List<projectBusinessesRelationRequest>? projectBusinessesRelations { get; set; }
        public List<projectPeopleRelationRequest>? projectPeopleRelations { get; set; }
    }
}
