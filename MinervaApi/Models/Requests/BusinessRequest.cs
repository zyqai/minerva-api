namespace Minerva.Models.Requests
{
    public class BusinessRequest
    {
        public int? BusinessId { get; set; }
        public int? TenantId { get; set; }
        public string? BusinessName { get; set; }
        public string? BusinessAddress { get; set; }
        public string? BusinessAddress1 { get; set; }
        public string? BusinessType { get; set; }
        public string? Industry { get; set; }
        public decimal? AnnualRevenue { get; set; }
        public DateTime? IncorporationDate { get; set; }
        public string? BusinessRegistrationNumber { get; set; }
        public string? RootDocumentFolder { get; set; }
    }
}
