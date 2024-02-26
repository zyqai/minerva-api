namespace MinervaApi.Models.Requests
{
    public class DocumentClassificationRequest
    {
        public int DocumentClassificationAutoId { get; set; }
        public int DocumentClassificationId { get; set; }
        public int TenantId { get; set; }
        public string? DocumentClassificationName { get; set; }
    }
}
