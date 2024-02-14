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
                        personaName = reader.GetValue(1).ToString(),
                    };
                    Personas.Add(Persona);
                }

            }
            return Personas;
        }
    }
}
