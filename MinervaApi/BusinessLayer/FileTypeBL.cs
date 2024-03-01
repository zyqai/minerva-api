using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models.Requests;
using MinervaApi.Models;

namespace Minerva.BusinessLayer
{
    public class FileTypeBL : IFileTypeBL
    {

        IFileTypeRepository Filetyperepository;
        public FileTypeBL(IFileTypeRepository _repository)
        {
            Filetyperepository = _repository;
        }


        public Task<bool> DeleteFileTypes(int FileTypeAutoId)
        {
            return Filetyperepository.DeleteFileType(FileTypeAutoId);
        }

        public Task<FileTypeResponse?> GetALLFileTypes()
        {
            return Filetyperepository.GetALLFileTypesAsync();
        }

        public Task<FileType?> GetFileType(int FileTypeAutoId)
        {
            return Filetyperepository.GetFileTypeAsync(FileTypeAutoId);
        }

        public Task<bool> SaveFileType(FileTypeRequest request)
        {
            FileType fileType = Mapping(request);
            return Filetyperepository.SaveFileType(fileType);
        }

        public Task<bool> UpdateFileTypes(FileTypeRequest request)
        {
            FileType fileType = Mapping(request);
            return Filetyperepository.UpdateFileType(fileType);
        }

        Task<bool> IFileTypeBL.DeleteFileTypes(int FileTypeAutoId)
        {
            throw new NotImplementedException();
        }

        private FileType Mapping(FileTypeRequest request)
        {
            FileType ft = new FileType();

            ft.FileTypeAutoId= request.FileTypeAutoId;

            ft.FileTypeId = request.FileTypeId;

            ft.TenantId = request.TenantId;

            ft.FileTypeName = request.FileTypeName;

            return ft;
        }

        Task<bool> IFileTypeBL.SaveFileType(StatesRequest request)
        {
            throw new NotImplementedException();
        }

        Task<bool> IFileTypeBL.UpdateFileTypes(StatesRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
