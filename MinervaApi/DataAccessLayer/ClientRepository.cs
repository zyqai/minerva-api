using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;

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
                        ClientId = Convert.ToInt32(reader["clientId"]),
                        UserId = reader["userId"].ToString(),
                        TenantId = Convert.ToInt32(reader["tenantId"]),
                        ClientName = reader["clientName"].ToString(),
                        ClientAddress = reader["clientAddress"].ToString(),
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
                        ModifiedBy = reader["modifiedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["modifiedBy"])
                    };
                    Clients.Add(Client);
                }

            }
            return Clients;
        }
        public async Task<bool> SaveClient(Client us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientCreate";
            AddParameters(command, us);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }
        public async Task<bool> UpdateClient(Client us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ClientUpdate";
            AddParameters(command, us);
            command.Parameters.AddWithValue("@p_modifiedBy", us.ModifiedBy);
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
            command.Parameters.AddWithValue("@p_phoneNumber", client.PhoneNumber);
            command.Parameters.AddWithValue("@p_email", client.Email);
            command.Parameters.AddWithValue("@p_preferredContact", client.PreferredContact);
            command.Parameters.AddWithValue("@p_creditScore", client.CreditScore);
            command.Parameters.AddWithValue("@p_lendabilityScore", client.LendabilityScore);
            command.Parameters.AddWithValue("@p_marriedStatus", client.MarriedStatus);
            command.Parameters.AddWithValue("@p_spouseClientId", client.SpouseClientId);
            command.Parameters.AddWithValue("@p_rootFolder", client.RootFolder);
            command.Parameters.AddWithValue("@p_createdBy", client.CreatedBy);
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
    }
}
