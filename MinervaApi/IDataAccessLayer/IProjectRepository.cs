using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IProjectRepository
    {
        public Task<Project?> GetProjectAsync(int Id_Projects);
        public Task<List<Project?>> GetAllProjectAsync();
        public Task<int> SaveProject(Project p);
        public Task<bool> UpdateProject(Project ps);
        public Task<bool> DeleteProject(int id);
        public Task<List<Project>?> GetProjectByTenantAsync(int tenantId);
        
    }
}
