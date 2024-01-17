using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Minerva.DataAccessLayer
{
    public class TenantRepositiry : ITenantRepositiry
    {
        MySqlDataSource database;
        public TenantRepositiry(MySqlDataSource _db)
        {
            database = _db;
        }

        public async Task<bool> DeleteTenant(int TenantId)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_TenantDelete";
            command.Parameters.AddWithValue("@p_tenantId", TenantId);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }

        public async Task<List<Tenant?>> GetALLAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_TenantGetAll";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        private async Task<IReadOnlyList<Tenant>> ReadAllAsync(MySqlDataReader mySqlDataReader)
        {
            var tenants = new List<Tenant>();
            using (mySqlDataReader)
            {
                while (await mySqlDataReader.ReadAsync())
                {
                    var tenant = new Tenant
                    {
                        TenantId = mySqlDataReader.GetInt32(0),
                        TenantName = mySqlDataReader.GetString(1),
                        TenantDomain = mySqlDataReader.GetString(2),
                        TenantLogoPath = mySqlDataReader.GetString(3),
                        TenantAddress = mySqlDataReader.GetString(4),
                        TenantPhone = mySqlDataReader.GetString(5),
                        TenantContactName = mySqlDataReader.GetString(6),
                        TenantContactEmail = mySqlDataReader.GetString(7),
                    };
                    tenants.Add(tenant);
                }
            }
            return tenants;

        }

        public async Task<Tenant?> GetTenantAsync(int? TenantId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_TenantGetById";
            command.Parameters.AddWithValue("@p_tenantId", TenantId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        public async Task<bool> SaveTenant(Tenant t)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_TenantCreate";
            AddUserParameters(command, t);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;

        }

        private void AddUserParameters(MySqlCommand command, Tenant t)
        {
            //tenantId
            command.Parameters.AddWithValue("@p_tenantName", t.TenantName);
            command.Parameters.AddWithValue("@p_tenantDomain", t.TenantDomain);
            command.Parameters.AddWithValue("@p_tenantLogoPath", t.TenantLogoPath);
            command.Parameters.AddWithValue("@p_tenantAddress", t.TenantAddress);
            command.Parameters.AddWithValue("@p_tenantPhone", t.TenantPhone);
            command.Parameters.AddWithValue("@p_tenantContactName", t.TenantContactName);
            command.Parameters.AddWithValue("@p_tenantContactEmail", t.TenantContactEmail);
        }

        public async Task<bool> UpdateTenant(Tenant t)
        {
            int i = 0;
            try
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();
                command.CommandText = @"USP_TenantUpdate";
                command.Parameters.AddWithValue("@p_tenantId", t.TenantId);
                AddUserParameters(command, t);
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
    }
}
