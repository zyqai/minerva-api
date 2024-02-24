namespace MinervaApi.Models
{
    public class Statuses
    {
        public int? statusAutoId { get; set; }
        public int? statusId { get; set;}
        public int? tenantId { get; set;}    
        public string? statusName { get; set;}
        public string? statusDescription { get; set;}
        public int? projectRequestTemplateStatus { get; set;}
    }
}
