﻿using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IPersonaRepository
    {
        public Task<List<Personas?>> GetALLPersonas();
        public Task<List<Persona>> GetPersonasByTenantId(int tenantId);
    }
}
