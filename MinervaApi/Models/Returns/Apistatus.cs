using MinervaApi.BusinessLayer;
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
    public class projectListDetails : Apistatus
    {
        public List<ProjectDetails>? responce { get; set; }
    }
    public class ProjectDetails
    {
        public int? projectId { get; set; }
        public int? tenantId { get; set; }
        public string? projectName { get; set; }
        public string? projectDescription { get; set; }
        public int? industryId { get; set; }
        public string? amount { get; set; }
        public int? loanTypeAutoId { get; set; }
        public string? purpose { get; set; }
        public int? statusAutoId { get; set;}
        public int? industrySectorAutoId { get; set;}
        public string? industrySector { get; set; }
        public string? industryDescription { get; set; }
        public string? assignedToUserId { get; set; }
        public string? assignedTousername { get; set; }
        public string? assignedToemail { get; set; }
        public string? assignedToName { get; set; }
        public int? statusId { get; set; }
        public string? statusName { get; set; }
        public string? statusDescription { get; set; }
        public string? loanType { get; set; }
        public string? loanTypeDescription { get; set; }
        public DateTime? createdOn { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class projectsRelationResponce : Apistatus
    {
        public Project? Project { get; set; }
        public Industrys? Industry { get; set; }
        public Statuses? Status { get; set; }
        public loanTypes? LoanType { get; set; }
        public List<ResponceprojectPeopleRelation?>? projectPeopleRelation { get; set; }
        public List<ResponceprojectBusinessesRelation?>? projectBusinessesRelation { get; set; }
        public List<Notes?> Notes { get; set; }
    }
    public class ResponceprojectPeopleRelation
    {
        public int? ProjectId { get; set; }
        public int? peopleId { get; set; }
        public int? tenantId { get; set; }
        public string? userId { get; set; }
        public string? clientName { get; set; }
        public string? clientAddress { get; set; }
        public string? phoneNumber { get; set; }
        public string? email { get; set; }
        public string? preferredContact { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public int? personaAutoId { get; set; }
        public int? projectPeopleId { get; set; }
        public int? personaId { get; set; }
        public string? personaName { get; set; }
        public string? primaryBorrower { get; set; }
        public string? projectPersona { get; set; }

    }
    public class ResponceprojectBusinessesRelation
    {
        public int? projectBusinessId { get; set; }
        public int? tenantId { get; set;}
        public int? projectId { get; set;}
        public int? businessId { get; set; }
        public string? businessName { get; set; }
        public string? businessType { get; set; }
    }
    public class Notes
    { 
        public int? projectNotesId { get; set; }
        public int? projectId { get; set; }
        public int? tenantId { get; set; }
        public string? notes { get; set; }
        public string? createdByUserId { get; set; }
        public DateTime? createdOn { get; set; }
        public string? CreatedByName { get; set; }

    }
}
