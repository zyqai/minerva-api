using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Returns;
using MinervaApi.Models.Requests;

namespace Minerva.BusinessLayer.Interface
{
    public interface IProjectsBL
    {
        public Task<List<Project?>> GetAllProjects();
        public Task<Project?> GetProjects(int Id_Projects);
        public Task<int> SaveProject(Models.Requests.ProjectRequest project);
        public Task<bool> UpdateProject(Models.Requests.ProjectRequest project);
        public Task<bool> DeleteProject(int Id_Projects);
        public Task<projectsResponce?> GetProjectDetails(int id);
        public Task<int> SaveProjectWithDetails(ProjectwithDetailsRequest request,string CreatedBy);
        public Task<projectListDetails> GetAllProjectsWithDetails(string? email);
        public Task<projectsRelationResponce> getProjectWithDetails(int id);
        public Task<Apistatus> SaveProjectRequest(ProjectRequestData request,string email);
    }
}
