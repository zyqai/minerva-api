using Minerva.Models.Returns;

namespace MinervaApi.Models.Returns
{
    public class ProjectRequestResponse
    {
        public int? projectRequestId { get; set; }
        public int? projectId { get; set; }
        public int? tenantId { get; set; }
        public string? projectRequestName { get; set; }
        public string? label { get; set; }
        public string? sentTo { get; set; }
        public string? sentcc { get; set; }
        public string? documentTypeName { get; set; }
        public string? statusName { get; set; }
    }
    public class ProjectRequestDetailsResponse:Apistatus
    { 
        public List<ProjectRequestResponse?>? ProjectRequest { get; set; }
    }
}
