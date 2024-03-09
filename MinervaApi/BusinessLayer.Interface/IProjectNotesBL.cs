using Minerva.Models.Requests;
using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IProjectNotesBL
    {
        public Task<int> SaveProjectNotes(ProjectNotesRequest request);
        public Task<ProjectNotes?> GetProjectNotes(int ProjectNotesId);
        public Task<ProjectNotesResponse?> GetALLProjectNotes();
        public Task<bool> UpdateProjectNotes(ProjectNotesRequest request);
        public Task<bool> DeleteProjectNotes(int ProjectNotesId);

    }
}
