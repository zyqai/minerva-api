using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;

namespace Minerva.DataAccessLayer
{
    public class PersonaRepository : IPersonaRepository
    {
        MySqlDataSource database;
        public PersonaRepository(MySqlDataSource database)
        {
            this.database = database;
        }
        public async Task<List<Personas?>> GetALLPersonas()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_PersonasGetAll";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<List<Personas?>> GetALLProjectPersonas(int projectPersona)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.Parameters.AddWithValue("@in_projectPersona", projectPersona);
            command.CommandText = @"USP_PersonasProjectGetAll";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<List<Persona>> GetPersonasByTenantId(int tenantId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_TenentPersonas";
            command.Parameters.AddWithValue("@p_tenantId", tenantId);
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAlPersonalAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        private async Task<IReadOnlyList<Personas>> ReadAllAsync(MySqlDataReader reader)
        {
            var Personas = new List<Personas>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var Persona = new Personas
                    {
                        personaId = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                        personaName = reader.GetValue(3).ToString(),
                    };
                    Personas.Add(Persona);
                }

            }
            return Personas;
        }
        private async Task<IReadOnlyList<Persona>> ReadAlPersonalAsync(MySqlDataReader reader)
        {
            var Personas = new List<Persona>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var Persona = new Persona
                    {
                        personaAutoId = reader["personaAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaAutoId"]),
                        personaId = reader["personaId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaId"]),
                        tenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                        projectPersona = reader["projectPersona"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectPersona"]),
                        personaName = reader["personaName"] == DBNull.Value ?string.Empty: reader["personaName"].ToString(),
                    };
                    Personas.Add(Persona);
                }

            }
            return Personas;
        }
    }
}
