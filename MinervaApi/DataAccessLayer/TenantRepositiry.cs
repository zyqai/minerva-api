using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Responce;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Minerva.DataAccessLayer
{
    public class TenantRepositiry : ITenantRepositiry
    {
        MySqlDataSource database;
        IBusinessRepository business;
        IClientRepository client;
        public TenantRepositiry(MySqlDataSource _db, IBusinessRepository _business,IClientRepository _client)
        {
            database = _db;
            business = _business;
            client = _client;
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
                        TenantId = mySqlDataReader.IsDBNull(0) ? 0 : mySqlDataReader.GetInt32(0),
                        TenantName = mySqlDataReader.IsDBNull(1) ? null : mySqlDataReader.GetString(1),
                        TenantDomain = mySqlDataReader.IsDBNull(2) ? null : mySqlDataReader.GetString(2),
                        TenantLogoPath = mySqlDataReader.IsDBNull(3) ? null : mySqlDataReader.GetString(3),
                        TenantAddress = mySqlDataReader.IsDBNull(4) ? null : mySqlDataReader.GetString(4),
                        TenantAddress1 = mySqlDataReader.IsDBNull(5) ? null : mySqlDataReader.GetString(5),
                        TenantPhone = mySqlDataReader.IsDBNull(6) ? null : mySqlDataReader.GetString(6),
                        TenantContactName = mySqlDataReader.IsDBNull(7) ? null : mySqlDataReader.GetString(7),
                        TenantContactEmail = mySqlDataReader.IsDBNull(8) ? null : mySqlDataReader.GetString(8),
                        PostalCode = mySqlDataReader.IsDBNull(9) ? null : mySqlDataReader.GetString(9),
                        City = mySqlDataReader.IsDBNull(10) ? null : mySqlDataReader.GetString(10),
                        stateid= mySqlDataReader.IsDBNull(11) ? null : mySqlDataReader.GetInt32(11),
                        CreatedBY= mySqlDataReader.IsDBNull(11) ? null : mySqlDataReader["CreatedBY"].ToString(),
                        UpdatedBY= mySqlDataReader.IsDBNull(11) ? null : mySqlDataReader["modifiedBy"].ToString(),
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

        public async Task<int> SaveTenant(Tenant t)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_TenantCreate";
           
            
            AddUserParameters(command, t);
            MySqlParameter outputParameter = new MySqlParameter("@p_last_insert_id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.AddWithValue("@p_createdBy", t.CreatedBY);

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

        private void AddUserParameters(MySqlCommand command, Tenant t)
        {
            //tenantId
            command.Parameters.AddWithValue("@p_tenantName", t.TenantName);
            command.Parameters.AddWithValue("@p_tenantDomain", t.TenantDomain);
            command.Parameters.AddWithValue("@p_tenantLogoPath", t.TenantLogoPath);
            command.Parameters.AddWithValue("@p_tenantAddress", t.TenantAddress);
            command.Parameters.AddWithValue("@p_tenantAddress1", t.TenantAddress1);
            command.Parameters.AddWithValue("@p_tenantPhone", t.TenantPhone);
            command.Parameters.AddWithValue("@p_tenantContactName", t.TenantContactName);
            command.Parameters.AddWithValue("@p_tenantContactEmail", t.TenantContactEmail);
            command.Parameters.AddWithValue("@p_postalCode", t.PostalCode);
            command.Parameters.AddWithValue("@p_city", t.City);
            command.Parameters.AddWithValue("@p_stateId", t.stateid);
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
                command.Parameters.AddWithValue("@p_modifiedBy", t.UpdatedBY);
                AddUserParameters(command, t);
                command.CommandType = CommandType.StoredProcedure;
                i = await command.ExecuteNonQueryAsync();
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return i >= 1 ? true : false;
        }

        public async Task<TenantBusiness> BusinessesForTenant(int tenantId)
        {
            TenantBusiness tenant=new TenantBusiness ();
            tenant.tenant = await GetTenantAsync(tenantId);
            tenant.business = await business.GetAllBussinessAsynctenant(tenantId);
            return tenant;
        }
        public async Task<PeopleBusiness> PeoplesForTenant(int tenantId)
        {
            PeopleBusiness peoples=new PeopleBusiness ();
            peoples.tenant = await GetTenantAsync(tenantId);
            peoples.peoples = await client.GetAllpeoplesAsynctenant(tenantId);
            return peoples;
        }
    }
}
