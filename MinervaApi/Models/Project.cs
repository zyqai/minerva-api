namespace Minerva.Models
{
     public class Project
     {
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public int? IndustryId { get; set; }
        public string? Amount { get; set; }
        public string? Purpose { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? AssignedToUserId { get; set; }
        public string? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int? LoanTypeAutoId { get; set; }
        public int? StatusAutoId { get; set; }
        public string? ProjectFilesPath { get; set; }
    }
}