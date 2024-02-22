namespace Minerva.Models
{
    public class CBRelation
    {
        public int? clientBusinessId { get; set; }
        public int? clientId { get; set; }
        public int? businessId { get; set; }
        public int? personaId { get; set; }
        public int? tenantId { get; set; }
        public string? details { get; set; }
    }
}
