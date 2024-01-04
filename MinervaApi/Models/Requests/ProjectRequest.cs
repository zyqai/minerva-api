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
    }
}
