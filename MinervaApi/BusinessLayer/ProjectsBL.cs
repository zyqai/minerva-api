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
        IUserRepository userRepository;
        public ProjectsBL(IProjectRepository _repository, IUserRepository user)
        {
            PorjectRepository = _repository;
            userRepository = user;
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
            return await PorjectRepository.SaveProject(project);
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
    }
}
