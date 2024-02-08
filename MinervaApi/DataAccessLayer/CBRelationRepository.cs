using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MinervaApi.ExternalApi;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Minerva.DataAccessLayer
{
    public class CBRelationRepository : ICBRelationRepository
    {
        MySqlDataSource con;
        public CBRelationRepository(MySqlDataSource _mySql)
        {
            con = _mySql;
        }

        public async Task<CBRelation?> GetAync(int? id)
        {
            using var connection = await con.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientBusinessRelationSelect";
            command.Parameters.AddWithValue("@in_clientBusinessId", id);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }
        public async Task<int?> Save(CBRelation? t)
        {
            using var connection = con.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientBusinessRelationInsert";
            AddUserParameters(command, t);
            MySqlParameter outputParameter = new MySqlParameter("@p_last_insert_id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputParameter);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            int lastInsertId = Convert.ToInt32(outputParameter.Value);
            connection.Close();
            if (i > 0)
            {
                i = lastInsertId;
            }
            return i;
        }

        private async Task<List<CBRelation>> ReadAllAsync(MySqlDataReader reader)
        {
            var res = new List<CBRelation>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var relation = new CBRelation
                    {
                        clientBusinessId= reader["clientBusinessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["clientBusinessId"]),
                        clientId = reader["clientId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["clientId"]),
                        businessId = reader["businessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["businessId"]),
                        personaId = reader["personaId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaId"]),
                    };
                    res.Add(relation);
                }
            }
            return res;
        }
        private void AddUserParameters(MySqlCommand command, CBRelation t)
        {
            command.Parameters.AddWithValue("@in_clientId", t.clientId);
            command.Parameters.AddWithValue("@in_businessId", t.businessId);
            command.Parameters.AddWithValue("@in_personaId", t.personaId);
        }

        public async Task<bool> Update(CBRelation relation)
        {
            int i = 0;
            try
            {
                using var connection = con.OpenConnection();
                using var command = connection.CreateCommand();
                command.CommandText = @"USP_ClientBusinessRelationUpdate";
                command.Parameters.AddWithValue("@in_clientBusinessId", relation.clientBusinessId);
                AddUserParameters(command, relation);
                command.CommandType = CommandType.StoredProcedure;
                i = await command.ExecuteNonQueryAsync();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i >= 1 ? true : false;
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = con.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientBusinessRelationDelete";
            command.Parameters.AddWithValue("@in_clientBusinessId", id);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }

        public async Task<List<CBRelation?>> GelAllAync()
        {
            using var connection = await con.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientBusinessRelationSelectALL";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result;
        }
    }
}
