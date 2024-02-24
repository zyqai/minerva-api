using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IFileTypeRepository
    {
        public Task<FileType?> GetFileTypeAsync(int? FileTypeAutoId);
        public Task<List<FileType?>> GetALLFileTypesAsync();
        public Task<bool> SaveFileType(FileType us);
        public Task<bool> UpdateFileType(FileType us);
        public Task<bool> DeleteFileType(int? FileTypeAutoId);
    }
}
