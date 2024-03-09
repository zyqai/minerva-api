namespace Minerva.Models.Requests
{
    public class ProjectNotesRequest
    {
        public int ProjectNotesId { get; set; }
        public int? ProjectId { get; set; }
        public int? TenantId { get; set; }
        public string Notes { get; set; }
        public string? createdByUserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedByUserId { get; set; }

    }
}
