namespace MinervaApi.Models.Requests
{
    public class FileTypeRequest
    {
        public int FileTypeAutoId { get; set; } 
        public int FileTypeId { get; set; }
        public int TenantId { get; set; } 
        public string FileTypeName { get; set; }

    }
}
