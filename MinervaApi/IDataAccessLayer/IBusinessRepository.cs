using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IBusinessRepository
    {
        public bool SaveBusiness(Business bs);
    }
}
