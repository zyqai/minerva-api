using MinervaApi.Models;

namespace Minerva.Models.Returns
{
    public class Apistatus
    {
        public string? code { get; set; }
        public string? message { get; set; }
    }
    public class projectPeopleRelationResponcestatus
    {
        public string? code { get; set; }
        public string? message { get; set; }
        public List<projectPeopleRelation>? responce { get; set; }
    }
    public class projectPeopleRelationResponce
    {
        public Apistatus? status { get; set; }
        public projectPeopleRelation? responce { get; set; }
    }

    public class projectsResponce :Apistatus
    { 
        public Project? Project { get; set; }
        public Industrys? Industry { get; set; }
        public Statuses? Status { get; set; }
        public loanTypes? LoanType { get; set; }
    }

}
