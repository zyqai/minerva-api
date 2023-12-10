namespace Minerva.Models
{
     public class Requests
     {
        public int RequestId  { get; set; }
        public String? TaskAction { get; set; }
        public String? RequestType { get; set; }
        public String? FileName { get; set; }
        public long? LoanAmount { get; set; }
        public String? FileDescription { get; set; }
        public String? StaffNotes { get;set; }
        public int BorrowerId { get; set; }
        public int BusinessId { get; set; }
        public String? StartDate { get; set; }
        public String? DesiredClosingDate { get; set; }
        public int AssignedTo { get; set; }
        public String? Status { get; set; }
        public String? DateCreated { get; set; }
        public String? LastUpdated { get; set; }
        public String[]? FundingUses { get; set; }

     }
}