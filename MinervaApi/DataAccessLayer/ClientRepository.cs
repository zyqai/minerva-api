using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Responce;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Minerva.DataAccessLayer
{
    public class ClientRepository : IClientRepository
    {
        MySqlDataSource database;
        public ClientRepository(MySqlDataSource database)
        {
            this.database = database;
        }
        public async Task<Client?> GetClientAsync(int? id)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClinetGetById";
            command.Parameters.AddWithValue("@p_clientId", id);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }
        public async Task<List<Client?>> GetALLClientsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClinetGetAll";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return (List<Client?>)result;
        }
        private async Task<IReadOnlyList<Client>> ReadAllAsync(MySqlDataReader reader)
        {
            var Clients = new List<Client>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var Client = new Client
                    {
                        ClientId = reader["clientId"] == DBNull.Value ? (int?)null :  Convert.ToInt32(reader["clientId"]),
                        UserId = reader["userId"].ToString(),
                        TenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                        ClientName = reader["clientName"].ToString(),
                        ClientAddress = reader["clientAddress"].ToString(),
                        firstName = reader["firstName"].ToString(),
                        lastName = reader["lastName"].ToString(),
                        dob = reader["dob"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["dob"]),
                        socialsecuritynumber= reader["socialsecuritynumber"].ToString(),
                        postalnumber = reader["postalnumber"].ToString(),
                        stateid = reader["stateid"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["stateid"]),
                        PhoneNumber = reader["phoneNumber"].ToString(),
                        Email = reader["email"].ToString(),
                        PreferredContact = reader["preferredContact"].ToString(),
                        CreditScore = reader["creditScore"].ToString(),
                        LendabilityScore = reader["lendabilityScore"].ToString(),
                        MarriedStatus = reader["marriedStatus"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["marriedStatus"]),
                        SpouseClientId = reader["spouseClientId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["spouseClientId"]),
                        RootFolder = reader["rootFolder"].ToString(),
                        CreatedTime = reader["createdTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["createdTime"]),
                        ModifiedTime = reader["modifiedTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["modifiedTime"]),
                        CreatedBy = reader["createdBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["createdBy"]),
                        ModifiedBy = reader["modifiedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["modifiedBy"]),
                        City = reader["city"].ToString(),
                        ClientAddress1 = reader["clientAddress1"].ToString(),

                    };
                    Clients.Add(Client);
                }

            }
            return Clients;
        }
        public async Task<int> SaveClient(Client us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientCreate";
            MySqlParameter outputParameter = new MySqlParameter("@p_last_insert_id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputParameter);

            AddParameters(command, us);
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
        public async Task<bool> UpdateClient(Client us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientUpdate";
            AddParameters(command, us);
            command.Parameters.AddWithValue("@p_modifiedBy", us.ModifiedBy);
            command.Parameters.AddWithValue("@p_clientId", us.ClientId);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }

        private void AddParameters(MySqlCommand command, Client client)
        {
            command.Parameters.AddWithValue("@p_userId", client.UserId);
            command.Parameters.AddWithValue("@p_tenantId", client.TenantId);
            command.Parameters.AddWithValue("@p_clientName", client.ClientName);
            command.Parameters.AddWithValue("@p_clientAddress", client.ClientAddress);
            command.Parameters.AddWithValue("@p_clientAddress1", client.ClientAddress1);

            command.Parameters.AddWithValue("@p_firstName", client.firstName);
            command.Parameters.AddWithValue("@p_lastName", client.lastName);
            command.Parameters.AddWithValue("@p_dob", client.dob);
            command.Parameters.AddWithValue("@p_socialsecuritynumber", client.socialsecuritynumber);
            command.Parameters.AddWithValue("@p_postalnumber", client.postalnumber);
            command.Parameters.AddWithValue("@p_stateid", client.stateid);

            command.Parameters.AddWithValue("@p_phoneNumber", client.PhoneNumber);
            command.Parameters.AddWithValue("@p_email", client.Email);
            command.Parameters.AddWithValue("@p_preferredContact", client.PreferredContact);
            command.Parameters.AddWithValue("@p_creditScore", client.CreditScore);
            command.Parameters.AddWithValue("@p_lendabilityScore", client.LendabilityScore);
            command.Parameters.AddWithValue("@p_marriedStatus", client.MarriedStatus);
            command.Parameters.AddWithValue("@p_spouseClientId", client.SpouseClientId);
            command.Parameters.AddWithValue("@p_rootFolder", client.RootFolder);
            command.Parameters.AddWithValue("@p_createdBy", client.CreatedBy);
            command.Parameters.AddWithValue("@p_city", client.City);
        }
        public async Task<bool> DeleteClient(int? Id)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientDelete";
            command.Parameters.AddWithValue("@p_clientId", Id);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }
        public async Task<List<Client>> GetAllpeoplesAsynctenant(int tenantId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientsForTenant";
            command.Parameters.AddWithValue("@in_tenantId", tenantId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.ToList();
        }

        public async Task<List<ClientPersonas>> GetClientPersonasAsync(int? businessId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientPersonasByBusinessId";
            command.Parameters.AddWithValue("@in_businessId", businessId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadClientPersonasAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.ToList();
        }

        private async Task<IReadOnlyList<ClientPersonas>>  ReadClientPersonasAsync(MySqlDataReader reader)
        {
            var Clients = new List<ClientPersonas>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var Client = new ClientPersonas
                    {
                        ClientId = reader["clientId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["clientId"]),
                        UserId = reader["userId"].ToString(),
                        TenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                        ClientName = reader["clientName"].ToString(),
                        firstName = reader["firstName"].ToString(),
                        lastName = reader["lastName"].ToString(),
                        stateid = reader["stateid"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["stateid"]),
                        PhoneNumber = reader["phoneNumber"].ToString(),
                        Email = reader["email"].ToString(),
                        clientBusinessId= reader["clientBusinessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["clientBusinessId"])
                    };
                    Client.Personas = new Personas();
                    Client.Personas.personaId = reader["personaId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaId"]);
                    Client.Personas.personaName= reader["personaName"].ToString();
                    Clients.Add(Client);
                }

            }
            return Clients;
        }
    }
}
