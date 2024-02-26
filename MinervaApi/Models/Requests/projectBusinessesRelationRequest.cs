namespace MinervaApi.Models.Requests
{
    public class projectBusinessesRelationRequest
    {
        public int? projectBusinessId { get; set; }
        public int? tenantId { get; set; }
        public int? projectId { get; set; }
        public int? businessId { get; set; }
        public int? primaryForLoan { get; set; }
        public DateTime? modifiedOn { get; set; }
        public string? modifiedBy { get; set; }
    }
}
