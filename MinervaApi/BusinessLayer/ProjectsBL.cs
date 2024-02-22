using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;
using MinervaApi.DataAccessLayer;

namespace Minerva.BusinessLayer
{
    public class ProjectsBL : IProjectsBL
    {
        IProjectRepository PorjectRepository;
        public ProjectsBL(IProjectRepository _repository)
        {
            PorjectRepository = _repository;
        }
        public Task<Project?> GetProjects(int Id_Projects)
        {
            return PorjectRepository.GetProjectAsync(Id_Projects);
        }

        public Task<bool> SaveProject(ProjectRequest request)
        {
            Project project = Mapping(request);
            return PorjectRepository.SaveProject(project);
        }

        public Task<List<Project?>> GetAllProjects()
        {
            return PorjectRepository.GetAllProjectAsync();
        }
        private Project Mapping(ProjectRequest request)
        {
            Project p = new Project();
            p.ProjectId=request.ProjectId;
            p.TenantId=request.TenantId;
            p.ProjectName=request.ProjectName;
            p.ProjectDescription=request.ProjectDescription;
            p.IndustryId=request.IndustryId;
            p.Amount    =request.Amount;
            p.Purpose =request.Purpose;
            p.CreatedDateTime =request.CreatedDateTime;
            p.CreatedByUserId   =request.CreatedByUserId;
            p.AssignedToUserId =request.AssignedToUserId;
            p.ModifiedByUserId =request.ModifiedByUserId;
            p.ModifiedDateTime= request.ModifiedDateTime;
            p.LoanTypeAutoId =request.LoanTypeAutoId;
            p.StatusAutoId =request.StatusAutoId;
            p.ProjectFilesPath = request.ProjectFilesPath;
            return p;
        }
        public Task<bool> UpdateProject(ProjectRequest request)
        {
            Project project = Mapping(request);
            return PorjectRepository.UpdateProject(project);
        }
        public Task<bool> DeleteProject(int id)
        { 
            return PorjectRepository.DeleteProject(id);
        }
    }
}
