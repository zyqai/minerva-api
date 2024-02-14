using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IPersona
    {
        public Task<List<Personas?>> GetALLPersonas();
    }
}
