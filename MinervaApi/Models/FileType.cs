using Minerva.Models.Returns;

namespace Minerva.Models
{
    public class FileType
    {
        public int FileTypeAutoId { get; set; }
        public int FileTypeId { get; set; }
        public int TenantId { get; set; }
        public string? FileTypeName { get; set; }

    }

    public class FileTypeResponse :Apistatus
    {
        public List<FileType>? FileTypes { get; set; }
    }
}
