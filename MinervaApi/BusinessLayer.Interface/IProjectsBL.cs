using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IProjectsBL
    {
        public Task<List<Project?>> GetAllProjects();
        public Task<Project?> GetProjects(int Id_Projects);
        public Task<int> SaveProject(Models.Requests.ProjectRequest project);
        public Task<bool> UpdateProject(Models.Requests.ProjectRequest project);
        public Task<bool> DeleteProject(int Id_Projects);
    }
}
