using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Returns;
using MinervaApi.DataAccessLayer;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
using MinervaApi.Models.Requests;
using System.Linq;

namespace Minerva.BusinessLayer
{
    public class ProjectsBL : IProjectsBL
    {
        IProjectRepository PorjectRepository;
        IUserRepository userRepository;
        IMasterRepository masterRepository;
        IprojectBusinessesRelationRepository Ibrr;
        IprojectPeopleRelationRepository Iprr;
        public ProjectsBL(IProjectRepository _repository, IUserRepository user, IMasterRepository _master, IprojectBusinessesRelationRepository _ipbrr,IprojectPeopleRelationRepository iproject)  
        {
            PorjectRepository = _repository;
            userRepository = user;
            masterRepository = _master;
            Ibrr = _ipbrr;
            Iprr = iproject;
        }
        public Task<Project?> GetProjects(int Id_Projects)
        {
            return PorjectRepository.GetProjectAsync(Id_Projects);
        }

        public async Task<int> SaveProject(ProjectRequest request)
        {
            var _user = await userRepository.GetuserusingUserNameAsync(request.CreatedByUserId);
            request.CreatedByUserId = _user?.UserId;
            Project project = Mapping(request);
            project.TenantId = _user?.TenantId;

            return await PorjectRepository.SaveProject(project);
        }

        public Task<List<Project?>> GetAllProjects()
        {
            return PorjectRepository.GetAllProjectAsync();
        }
        private Project Mapping(ProjectRequest request)
        {
            Project p = new Project();
            p.ProjectId = request.ProjectId;
            p.TenantId = request.TenantId;
            p.ProjectName = request.ProjectName;
            p.ProjectDescription = request.ProjectDescription;
            p.IndustryId = request.IndustryId;
            p.Amount = request.Amount;
            p.Purpose = request.Purpose;
            p.CreatedDateTime = request.CreatedDateTime;
            p.CreatedByUserId = request.CreatedByUserId;
            p.AssignedToUserId = request.AssignedToUserId;
            p.ModifiedByUserId = request.ModifiedByUserId;
            p.ModifiedDateTime = request.ModifiedDateTime;
            p.LoanTypeAutoId = request.LoanTypeAutoId;
            p.StatusAutoId = request.StatusAutoId;
            p.ProjectFilesPath = request.ProjectFilesPath;
            p.ProjectStartDate=request.ProjectStartDate;
            p.DesiredClosedDate=request.DesiredClosedDate;
            p.Notes=request.Notes;
            p.PrimaryBorrower=request.PrimaryBorrower;
            p.PrimaryBusiness = request.PrimaryBusiness;
            return p;
        }
        public async Task<bool> UpdateProject(ProjectRequest request)
        {
            var _user = await userRepository.GetuserusingUserNameAsync(request.ModifiedByUserId);
            request.ModifiedByUserId = _user?.UserId;
            Project project = Mapping(request);
            return await PorjectRepository.UpdateProject(project);
        }
        public Task<bool> DeleteProject(int id)
        {
            return PorjectRepository.DeleteProject(id);
        }

        public async Task<projectsResponce?> GetProjectDetails(int id)
        {
            projectsResponce projectsResponce = new projectsResponce();
            projectsResponce.Project = await PorjectRepository.GetProjectAsync(id);
            if (projectsResponce.Project != null)
            {
                projectsResponce.code = "206";
                projectsResponce.message = "responce available";
                projectsResponce.Status = await masterRepository.GetStatusByIdAsync(projectsResponce?.Project?.StatusAutoId);
                projectsResponce.Industry = await masterRepository.GetIndustrysByIdAsync(projectsResponce.Project?.IndustryId);
                projectsResponce.LoanType = await masterRepository.GetloanTypesByIdAsync(projectsResponce.Project?.LoanTypeAutoId);
            }
            else
            {
                projectsResponce.code = "201";
                projectsResponce.message = "no content";
            }
            return projectsResponce;
        }

        public async Task<int> SaveProjectWithDetails(ProjectwithDetailsRequest request,string CreatedBy)
        {
            projectPeopleRelationRequest ?projectPeople = request?.projectPeopleRelations?.Where(w => w.primaryBorrower == 1).Select(s => s).FirstOrDefault();
            projectBusinessesRelationRequest ?projectBusinesses=request?.projectBusinessesRelations?.Where(w=>w.primaryForLoan==1).Select(s => s).FirstOrDefault();
            User? user = await userRepository.GetuserusingUserNameAsync(CreatedBy);
            int i= await PorjectRepository.SaveProjectWithDetails(request, user,projectPeople?.peopleId,projectPeople?.personaAutoId, projectBusinesses?.businessId);
            if (request?.projectBusinessesRelations?.Count() > 1)
            {
                foreach (var item in request?.projectBusinessesRelations?.Where(w => w.primaryForLoan != 1))
                {
                    item.projectId = i;
                    item.tenantId = user?.TenantId;
                    int ir=await Ibrr.CreateProjectBusinessRelation(item);
                }
            }
            if(request?.projectPeopleRelations?.Count() > 1)
            {
                foreach (var item in request?.projectPeopleRelations?.Where(w => w.primaryBorrower != 1))
                {
                    projectPeopleRelation project = new projectPeopleRelation 
                    {
                        peopleId = item.peopleId,
                        personaAutoId = item.personaAutoId,
                        primaryBorrower = item.primaryBorrower,
                        projectId = i,
                        projectPeopleId=item.projectPeopleId,
                        tenantId = user?.TenantId
                };
                    int ib = await Iprr.CreateprojectPeopleRelation(project);
                }
            }
            return i;
        }

        public async Task<projectListDetails> GetAllProjectsWithDetails(string? email)
        {
            projectListDetails projectList = new projectListDetails();
            User? user = await userRepository.GetuserusingUserNameAsync(email);
            List<ProjectDetails> projectListDetails = new List<ProjectDetails>();
            projectListDetails=await PorjectRepository.GetAllProjectsWithDetails(user?.TenantId);
            if (projectListDetails.Count > 0)
            {
                projectList.code = "200";
                projectList.message = "response available";
                projectList.responce = new List<ProjectDetails>();
                projectList.responce = projectListDetails.ToList();
            }
            else
            {
                projectList.code = "204";
                projectList.message = "No Content";
            }
            return projectList;
        }

        public async Task<projectsRelationResponce> getProjectWithDetails(int id)
        {
            projectsRelationResponce projectsResponce=new projectsRelationResponce ();
            projectsResponce.Project = await PorjectRepository.GetProjectAsync(id);
            if (projectsResponce.Project != null)
            {
                projectsResponce.code = "206";
                projectsResponce.message = "responce available";
                projectsResponce.Status = await masterRepository.GetStatusByIdAsync(projectsResponce?.Project?.StatusAutoId);
                projectsResponce.Industry = await masterRepository.GetIndustrysByIdAsync(projectsResponce.Project?.IndustryId);
                projectsResponce.LoanType = await masterRepository.GetloanTypesByIdAsync(projectsResponce.Project?.LoanTypeAutoId);
                projectsResponce.projectPeopleRelation = await Iprr.GetPeopleDetailsByProjectId(projectsResponce.Project?.ProjectId);
                projectsResponce.projectBusinessesRelation = await Ibrr.GetBusinessByProjectid(projectsResponce.Project?.ProjectId);
                projectsResponce.Notes = await PorjectRepository.GetNotesByProjectId(projectsResponce.Project?.ProjectId);
            }
            return projectsResponce;
        }

        public async Task<Apistatus> SaveProjectRequest(ProjectRequestData request, string email)
        {
            User ?user = await userRepository.GetuserusingUserNameAsync(email);
            if (user != null)
            {
                request.TenentId = user.TenantId;
            }
            return await PorjectRepository.SaveProjectRequest(request,user?.UserId);
        }

        public async Task<Apistatus> UpdateProjectRequest(ProjectRequestDetailUpdateData request, string? email)
        {
            User? user = await userRepository.GetuserusingUserNameAsync(email);
            return await PorjectRepository.UpdateProjectRequest(request, user?.UserId);
        }
    }
}
