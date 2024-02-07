using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models;

namespace Minerva.BusinessLayer
{
    public class PersonaBL : IPersona
    {
        IPersonaRepository IPersona;
        public PersonaBL(IPersonaRepository _per) 
        {
            IPersona = _per;
        }
        public Task<List<Personas?>> GetALLPersonas()
        {
            return IPersona.GetALLPersonas();
        }
    }
}
