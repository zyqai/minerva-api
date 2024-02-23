using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Responce;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Minerva.DataAccessLayer
{
    public class BusinessRepository : IBusinessRepository
    {
        MySqlDataSource database;
        public BusinessRepository(MySqlDataSource database)
        {
            this.database = database;
        }
        public async Task<int?> SaveBusiness(Business bs)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_BusinessCreate";
            AddUserParameters(command, bs);
            MySqlParameter outputParameter = new MySqlParameter("@p_last_insert_id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.AddWithValue("@in_CreateBy", bs.CreatedBy);
            command.Parameters.Add(outputParameter);
            command.CommandType = CommandType.StoredProcedure;
            int ?i = command.ExecuteNonQuery();
            int lastInsertId = Convert.ToInt32(outputParameter.Value);
            connection.Close();
            if (i == 1)
            {
                i = lastInsertId;
            }
            else
            {
                i = 0;
            }
            return i;
        }
        public async Task<List<Business?>> GetAllBussinessAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_BusinessesGet";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return (List<Business?>)result;
        }

        public async Task<Business?> GetBussinessAsync(int ?BusinessId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_BusinessGetID";
            command.Parameters.AddWithValue("@in_businessId", BusinessId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<IReadOnlyList<Business>> ReadAllAsync(MySqlDataReader reader)
        {
            var bu = new List<Business>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new Business
                    {
                        BusinessId = reader.GetInt32(0),
                        TenantId=reader.GetInt32(1),
                        BusinessName= reader.GetValue(2).ToString(),
                        BusinessAddress= reader.GetValue(3).ToString(),
                        BusinessType =reader.GetValue(4).ToString(),
                        Industry  =reader.GetValue(5).ToString(),
                        AnnualRevenue= reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
                        IncorporationDate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                        BusinessRegistrationNumber =reader.GetValue(8).ToString(),
                        RootDocumentFolder=reader.GetValue(9).ToString(),
                        BusinessAddress1 = reader.GetValue(10).ToString(),
                        //CreatedBy= reader["CreatedBy"].ToString(),
                        //UpdatedBy = reader["modifiedBy"].ToString()
                    };
                    bu.Add(user);
                }
            }
            return bu;
        }

        private void AddUserParameters(MySqlCommand command, Business bs)
        {
            command.Parameters.AddWithValue("@in_businessId", bs.BusinessId);
            command.Parameters.AddWithValue("@in_tenantId", bs.TenantId);
            command.Parameters.AddWithValue("@in_businessName", bs.BusinessName);
            command.Parameters.AddWithValue("@in_businessAddress", bs.BusinessAddress);
            command.Parameters.AddWithValue("@in_businessAddress1", bs.BusinessAddress1);
            command.Parameters.AddWithValue("@in_businessType", bs.BusinessType);
            command.Parameters.AddWithValue("@in_industry", bs.Industry);
            command.Parameters.AddWithValue("@in_annualRevenue", bs.AnnualRevenue);
            command.Parameters.AddWithValue("@in_incorporationDate", bs.IncorporationDate);
            command.Parameters.AddWithValue("@in_businessRegistrationNumber", bs.BusinessRegistrationNumber);
            command.Parameters.AddWithValue("@in_rootDocumentFolder", bs.RootDocumentFolder);
        }

        public bool UpdateBusiness(Business bs)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_businessUpdate";
            command.Parameters.AddWithValue("@in_updateBy", bs.UpdatedBy);
            AddUserParameters(command, bs);
            command.CommandType = CommandType.StoredProcedure;
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i >= 1 ? true : false;
        }
        public bool DeleteBusiness(int BusinesId)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_DeleteBusiness";
            command.Parameters.AddWithValue("@in_businessId", BusinesId);
            command.CommandType = CommandType.StoredProcedure;
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i >= 1 ? true : false;
        }

        public async Task<List<Business?>> GetAllBussinessAsynctenant(int tenantId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_BusinessesForTenant";
            command.Parameters.AddWithValue("@in_tenantId", tenantId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<List<BusinessPersonas>> GetBussinessPersonasAsync(int? clientId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_BusinessesPersonasByclientIds ";
            command.Parameters.AddWithValue("@p_clientId", clientId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllBussinessPersonasAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.ToList();
        }

        private async Task<List<BusinessPersonas>> ReadAllBussinessPersonasAsync(MySqlDataReader reader)
        {
            var bu =  new List<BusinessPersonas> ();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var b = new BusinessPersonas
                    {
                        BusinessId = reader["businessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["businessId"]),
                        TenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                        BusinessName = reader["businessName"].ToString(),
                        BusinessAddress = reader["businessAddress"].ToString(),
                        BusinessType = reader["businessType"].ToString(),
                        Industry = reader["industry"].ToString(),
                        clientBusinessId= reader["clientBusinessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["clientBusinessId"])
                    };
                    b.Personas = new Personas { 
                    personaId= reader["personaId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaId"]),
                    personaName= reader["personaName"].ToString()
                    };
                    bu.Add(b);
                }
            }
            return bu;
        }
    }
}
