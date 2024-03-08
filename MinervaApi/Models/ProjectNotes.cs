using Minerva.Models.Returns;

namespace Minerva.Models
{
    public class ProjectNotes
    {
        public int ProjectNotesId { get; set; }
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public string? Notes { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime? CreatedOn { get; set; }

    }

    public class ProjectNotesResponse : Apistatus
    {

        public List<ProjectNotes>? ProjectNotes { get; set; }

    }
}
