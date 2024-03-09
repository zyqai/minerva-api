using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.IDataAccessLayer
{
    public interface IProjectNotesRepository
    {
        public Task<ProjectNotes?> GetProjectNotesAsync(int? ProjectNotesId);
        public Task<ProjectNotesResponse?> GetALLProjectNotessAsync();
        public Task<int> SaveProjectNotes(ProjectNotes dt);
        public Task<bool> UpdateProjectNotes(ProjectNotes dt);
        public Task<bool> DeleteProjectNotes(int? ProjectNotesId);

    }
}
