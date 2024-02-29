namespace MinervaApi.Models.Requests
{
    public class ProjectNotes
    {
        public int ProjectNotesId { get; set; }
        public int ProjectId { get; set; }
        public int TenantId { get; set; }
        public string Notes { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
