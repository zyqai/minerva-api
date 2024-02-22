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
            command.CommandText = @"usp_peopleBusinessRelation";
            command.Parameters.AddWithValue("@p_peopleBusinessId", id);
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
            command.CommandText = @"usp_peopleBusinessRelationInsert";
            AddUserParameters(command, t);
            //MySqlParameter outputParameter = new MySqlParameter("@p_last_insert_id", SqlDbType.Int)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //command.Parameters.Add(outputParameter);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            //int lastInsertId = Convert.ToInt32(outputParameter.Value);
            connection.Close();
            //if (i > 0)
            //{
            //    i = lastInsertId;
            //}
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
                        clientBusinessId= reader["peopleBusinessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["peopleBusinessId"]),
                        clientId = reader["peopleId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["peopleId"]),
                        businessId = reader["businessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["businessId"]),
                        personaId = reader["personaId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaId"]),
                        tenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                        details = reader["details"] == DBNull.Value ?string.Empty : reader["details"].ToString(),
                    };
                    res.Add(relation);
                }
            }
            return res;
        }
        private void AddUserParameters(MySqlCommand command, CBRelation t)
        {
            command.Parameters.AddWithValue("@p_peopleId", t.clientId);
            command.Parameters.AddWithValue("@p_businessId", t.businessId);
            command.Parameters.AddWithValue("@p_personaAutoId", t.personaId);
            command.Parameters.AddWithValue("@p_tenantId", t.tenantId);
            command.Parameters.AddWithValue("@p_details", t.details);

        }

        public async Task<bool> Update(CBRelation relation)
        {
            int i = 0;
            try
            {
                using var connection = con.OpenConnection();
                using var command = connection.CreateCommand();
                command.CommandText = @"USP_peopleBusinessRelationUpdate";
                command.Parameters.AddWithValue("@p_peopleBusinessId", relation.clientBusinessId);
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
            command.CommandText = @"usp_peopleBusinessRelationDelete";
            command.Parameters.AddWithValue("@p_peopleBusinessId", id);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }

        public async Task<List<CBRelation?>> GelAllAync()
        {
            using var connection = await con.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_peopleBusinessRelations";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result;
        }
    }
}
