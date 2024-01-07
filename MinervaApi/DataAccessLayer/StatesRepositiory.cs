using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;

namespace Minerva.DataAccessLayer
{
    public class StatesRepositiory : IStatesRepository
    {
        MySqlDataSource database;
        public StatesRepositiory(MySqlDataSource _dataSource)
        {
            database = _dataSource;
        }

        public async Task<bool> DeleteState(int? id)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_DeleteState";
            command.Parameters.AddWithValue("@p_id", id);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }

        public async Task<List<States?>> GetALLStatesAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_GetStates";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }
        private async Task<IReadOnlyList<States>> ReadAllAsync(MySqlDataReader reader)
        {
            var states = new List<States>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var state = new States
                    {
                        id = Convert.ToInt32(reader["id"]),
                        Code = reader["code"].ToString(),
                        Name = reader["name"].ToString()
                    };
                    states.Add(state);
                }

            }
            return states;
        }
        public async Task<States?> GetStateAsync(int? id)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_GetState";
            command.Parameters.AddWithValue("@p_id", id);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        public async Task<bool> SaveState(States us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_InsertState";
            AddParameters(command, us);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }

        private void AddParameters(MySqlCommand command, States us)
        {
            command.Parameters.AddWithValue("@p_id", us.id);
            command.Parameters.AddWithValue("@p_code", us.Code);
            command.Parameters.AddWithValue("@p_name", us.Name);
        }

       public async Task<bool> UpdateState(States us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_InsertState";
            AddParameters(command, us);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }
    }
}
