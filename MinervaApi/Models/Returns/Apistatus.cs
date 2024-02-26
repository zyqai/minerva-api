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
    public class ProjectByPeople : Apistatus
    { 
        public Project? Project { get; set; }
        public List<Peoplesbyproject>? People { get; set; } 

    }
    public class Peoplesbyproject
    {
        public string? clientname { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? phonenumber { get; set; }
        public string? email { get; set; }
        public string? projectName { get; set; }
        public string? amount { get; set; }
        public int? personaAutoId { get; set; }
        public int? personaId { get; set; }
        public string? personaName { get; set; }
        public string? projectPersona { get; set; }
        public string? purpose { get; set; }
        public int? projectid { get; set; }
    }
    public class ProjectByBusiness : Apistatus
    { 
        public Project? Project { get; set; }
        public List<BusinessesByProject>? BusinessRelation { get; set; }
    }

    public class BusinessesByProject
    {
        public int? projectBusinessId { get; set; }
        public int? businessId { get; set; }
        public string? businessName { get; set; }
        public string? industry { get; set;}
        public string? annualRevenue { get; set; }
        public string? businessAddress { get; set; }
        public int? peopleid { get; set; }
        public string? clientName { get; set; }
        public int? projectId { get; set; }
        public string? projectName { get; set;}
    }


}
