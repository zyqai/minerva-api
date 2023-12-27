using Minerva.Models;
using Minerva.IDataAccessLayer;
using MySqlConnector;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Data;
using System.Collections.Generic;

namespace Minerva.DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        MySqlDataSource database;
        public UserRepository(MySqlDataSource database)
        {
            this.database = database;
        }


        public async Task<User?> GetuserAsync(string ?userId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_GetUserById";
            command.Parameters.AddWithValue("@p_userId", userId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }
        public async Task<List<User?>> GetALLAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_GetUsers";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }
        private async Task<IReadOnlyList<User>> ReadAllAsync(MySqlDataReader reader)
        {
            var users = new List<User>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        UserId = reader.GetValue(0).ToString(),
                        TenantId = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                        UserName = reader.GetValue(2).ToString(),
                        Email = reader.GetValue(3).ToString(),
                        IsActive = reader.GetInt16(4) == 1 ? true : false,
                        IsDeleted = reader.GetInt16(5) == 1 ? true : false,
                        CreateTime = reader.GetDateTime(6),
                        ModifiedTime = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                        CreatedBy = reader.GetValue(8).ToString(),
                        ModifiedBy = reader.GetValue(9).ToString(),
                        PhoneNumber = reader.GetValue(10).ToString(),
                        NotificationsEnabled = reader.GetInt16(11) == 1 ? true : false,
                        MfaEnabled = reader.GetInt16(12) == 1 ? true : false,
                        IsTenantUser = reader.GetInt32(13),
                        IsAdminUser = reader.GetInt32(14),


                    };
                    users.Add(user);
                    if (user.TenantId != 0)
                    {
                        user.Tenant = new Tenant
                        {
                            TenantId= reader.GetInt16(15),
                            TenantName= reader.GetString(16),
                            TenantDomain = reader.GetString(17),
                            TenantLogoPath = reader.GetString(18),
                            TenantAddress = reader.GetString(19),
                            TenantPhone = reader.GetString(20),
                            TenantContactName = reader.GetString(21),
                            TenantContactEmail = reader.GetString(22),
                        };
                    }
                }

            }
            return users;
        }
        public bool SaveUser(User us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_CreateUser";
            AddUserParameters(command, us);
            command.Parameters.AddWithValue("@p_createdBy", us.CreatedBy);
            command.CommandType = CommandType.StoredProcedure;
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateUser(User us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_UpdateUser";
            AddUserParameters(command, us);
            command.Parameters.AddWithValue("@p_modifiedBy", us.ModifiedBy);
            command.CommandType = CommandType.StoredProcedure;
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i >= 1 ? true : false;
        }
        private void AddUserParameters(MySqlCommand command, User us)
        {
            command.Parameters.AddWithValue("@p_userId", us.UserId);
            command.Parameters.AddWithValue("@p_tenantId", us.TenantId);
            command.Parameters.AddWithValue("@p_userName", us.UserName);
            command.Parameters.AddWithValue("@p_email", us.Email);
            command.Parameters.AddWithValue("@p_isActive", us.IsActive);
            command.Parameters.AddWithValue("@p_isDeleted", us.IsDeleted);
            command.Parameters.AddWithValue("@p_phoneNumber", us.PhoneNumber);
            command.Parameters.AddWithValue("@p_notificationsEnabled", us.NotificationsEnabled);
            command.Parameters.AddWithValue("@p_mfaEnabled", us.MfaEnabled);
            command.Parameters.AddWithValue("@p_isTenantUser", us.IsTenantUser);
            command.Parameters.AddWithValue("@p_isAdminUser", us.IsAdminUser);
        }
        public bool DeleteUser(string UserId)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_DeleteUser";
            command.Parameters.AddWithValue("@in_userId", UserId);
            command.CommandType = CommandType.StoredProcedure;
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i >= 1 ? true : false;
        }
    }
}
