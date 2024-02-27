using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Returns;

namespace Minerva.IDataAccessLayer
{
    public interface IProjectRepository
    {
        public Task<Project?> GetProjectAsync(int? Id_Projects);
        public Task<List<Project?>> GetAllProjectAsync();
        public Task<int> SaveProject(Project p);
        public Task<bool> UpdateProject(Project ps);
        public Task<bool> DeleteProject(int id);
        public Task<List<Project>?> GetProjectByTenantAsync(int tenantId);
        public Task<int> SaveProjectWithDetails(ProjectwithDetailsRequest request, User? user, int? peopleId, int? personaAutoId, int? businessId);
        public Task<List<ProjectDetails>> GetAllProjectsWithDetails(int? tenantId);
    }
}
