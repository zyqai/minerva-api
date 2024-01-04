using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IProjectRepository
    {
        public Task<Project?> GetProjectAsync(int Id_Projects);
        public Task<List<Project?>> GetAllProjectAsync();
        Task<bool> SaveProject(Project p);
        public Task<bool> UpdateProject(Project ps);
        public Task<bool> DeleteProject(int BusinesId);
    }
}
