namespace MinervaApi.Models.Requests
{
    public class projectPeopleRelationRequest
    {
        public int? projectPeopleId { get; set; }
        public int? tenantId { get; set; }
        public int? projectId { get; set; }
        public int? peopleId { get; set; }
        public int? primaryBorrower { get; set; }
        public int? personaAutoId { get; set; }
    }
}
