using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IFileTypeBL
    {
        public Task<bool> SaveFileType(StatesRequest request);
        public Task<FileType?> GetFileType(int FileTypeAutoId);
        public Task<List<FileType?>> GetALLFileTypes();
        public Task<bool> UpdateFileTypes(StatesRequest request);
        public Task<bool> DeleteFileTypes(int FileTypeAutoId);
    }
}
