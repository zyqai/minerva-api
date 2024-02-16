namespace Minerva.Models.Requests
{
    public class ProjectRequest
    {
        public int Id_Projects { get; set; }
        public string? Filename { get; set; }
        public decimal? Loanamount { get; set; }
        public string? Assignrdstaff { get; set; }
        public string? Filedescription { get; set; }
        public string? Staffnote { get; set; }
        public string? Primaryborrower { get; set; }
        public string? Primarybusiness { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Desiredclosingdate { get; set; }
        public string? Initialphase { get; set; }
        public DateTime CreateDateTime { get; set; }


        //public int ProjectId { get; set; }
        //public int TenantId { get; set; }
        //public string ProjectName { get; set; }
        //public string ProjectDescription { get; set; }
        //public int IndustryId { get; set; }
        //public string Amount { get; set; }
        //public string Purpose { get; set; }
        //public int CreatedByUserId { get; set; }
        //public DateTime CreatedDateTime { get; set; }
        //public int AssignedToUserId { get; set; }
        //public int ModifiedByUserId { get; set; }
        //public DateTime ModifiedDateTime { get; set; }
        //public int LoanTypeAutoId { get; set; }
        //public int StatusAutoId { get; set; }
        //public string ProjectFilesPath { get; set; }
    }
}
