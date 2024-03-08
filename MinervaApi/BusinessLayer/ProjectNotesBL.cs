using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using Minerva.DataAccessLayer;

namespace MinervaApi.BusinessLayer
{
    public class ProjectNotesBL:IProjectNotesBL
    {
        IProjectNotesRepository ProjectNotesrepository;
        IUserRepository UserRepository;

        public ProjectNotesBL(IProjectNotesRepository _repository, IUserRepository userRepository)
        {
            ProjectNotesrepository = _repository;
            UserRepository = userRepository;
        }


        public Task<ProjectNotesResponse?> GetALLProjectNotes()
        {
            return ProjectNotesrepository.GetALLProjectNotessAsync();
        }

        public Task<ProjectNotes?> GetProjectNotes(int ProjectNotesId)
        {
            return ProjectNotesrepository.GetProjectNotesAsync(ProjectNotesId);
        }

        public async Task<int> SaveProjectNotes(ProjectNotesRequest request)
        {
            User? user = await UserRepository.GetuserusingUserNameAsync(request.createdByUserId);
            request.createdByUserId = user?.UserId;
            request.TenantId = user?.TenantId;

            ProjectNotes ProjectNotes = Mapping(request);

            return await ProjectNotesrepository.SaveProjectNotes(ProjectNotes);
        }

        public async Task<bool> UpdateProjectNotes(ProjectNotesRequest request)
        {
            User? user = await UserRepository.GetuserusingUserNameAsync(request.ModifiedByUserId);

            request.createdByUserId = user?.UserId;
            request.TenantId = user?.TenantId;

            ProjectNotes ProjectNotes = Mapping(request);

            return await ProjectNotesrepository.UpdateProjectNotes(ProjectNotes);
        }

        Task<bool> IProjectNotesBL.DeleteProjectNotes(int ProjectNoteId)
        {
            return ProjectNotesrepository.DeleteProjectNotes(ProjectNoteId);
        }

        private ProjectNotes Mapping(ProjectNotesRequest request)
        {
            ProjectNotes dc = new ProjectNotes();

            dc.ProjectNotesId = request.ProjectNotesId;

            dc.ProjectId = request.ProjectId;

            dc.TenantId = request.TenantId;

            dc.Notes = request.Notes;

            dc.CreatedByUserId = request.createdByUserId;

            return dc;
        }
    }
}
