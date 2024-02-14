using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IPersonaRepository
    {
        public Task<List<Personas?>> GetALLPersonas();
    }
}
