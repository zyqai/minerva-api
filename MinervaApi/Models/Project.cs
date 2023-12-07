namespace Minerva.Models
{
     public class Project
     {
        public int ID { get; set; }

        public int FileId { get; set; }
        public String? Name { get; set; }
        public String? Lender { get; set; }
        public long Progress { get; set; }
        public String? DateOfLoan { get; set; }
        public String? fileOwner { get; set; }
        public long loanAmount { get; set; }
        public String? Phase { get; set; }
        public int ProbabilityToFund { get; set; }
        public String? DurationOfProcess { get; set; }
        public String[]? Tags { get; set; }
        public People[]? people { get; set; }
        public Business[]? businesses { get; set; }
     }
}