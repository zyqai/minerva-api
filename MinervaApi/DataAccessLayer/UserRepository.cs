using Minerva.Models;
using Minerva.IDataAccessLayer;
using MySqlConnector;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Data;
using System.Collections.Generic;
using MinervaApi.ExternalApi;
using static MinervaApi.ExternalApi.Keycloak;
using MinervaApi.IDataAccessLayer;

namespace Minerva.DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        MySqlDataSource database;
        IKeycloakApiService keycloak;
        public UserRepository(MySqlDataSource database, IKeycloakApiService keycloakApiService)
        {
            this.database = database;
            keycloak = keycloakApiService;
        }


        public async Task<User?> GetuserAsync(string? userId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_user";
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
            command.CommandText = @"usp_users";
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
                        NotificationsEnabled = reader.IsDBNull(11) ? false : (reader.GetInt16(11) == 1),
                        MfaEnabled = reader.IsDBNull(12) ? false : (reader.GetInt16(12) == 1),
                        IsTenantUser = reader.IsDBNull(13) ? 0 : reader.GetInt32(13),
                        IsAdminUser = reader.IsDBNull(14) ? 0 : reader.GetInt32(14),
                        FirstName = reader.IsDBNull(15) ? null : reader.GetValue(15).ToString(),
                        LastName = reader.IsDBNull(16) ? null : reader.GetValue(16).ToString(),
                        Roles = reader.IsDBNull(17) ? null : reader.GetValue(17).ToString(),


                    };
                    users.Add(user);
                    
                }

            }
            return users;
        }
        public async Task<string> SaveUser(User? us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_userInsert";
            AddUserParameters(command, us);
            command.Parameters.AddWithValue("@in_createdBy", us.CreatedBy);
            MySqlParameter outputParameter = new MySqlParameter("@p_last_insert_id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputParameter);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            string? lastInsertId = outputParameter.Value != null ? outputParameter.Value.ToString() : "";
            connection.Close();
            if (i > 0)
            {
                us =await GetuserAsync(lastInsertId);
                var res =keycloak.CreateUser(us);
                return lastInsertId;
            }
            return string.Empty;

        }
        public async Task<bool> UpdateUser(User us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_UserUpdate";
            AddUserParameters(command, us);
            command.Parameters.AddWithValue("@p_modifiedBy", us.ModifiedBy);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }
        private void AddUserParameters(MySqlCommand command, User us)
        {
            if (!string.IsNullOrEmpty(us.UserId))
            {
                command.Parameters.AddWithValue("@in_userId", us.UserId);
            }
            command.Parameters.AddWithValue("@in_tenantId", us.TenantId);
            command.Parameters.AddWithValue("@in_userName", us.UserName);
            command.Parameters.AddWithValue("@in_email", us.Email);
            command.Parameters.AddWithValue("@in_isActive", us.IsActive);
            command.Parameters.AddWithValue("@in_isDeleted", us.IsDeleted);
            command.Parameters.AddWithValue("@in_phoneNumber", us.PhoneNumber);
            command.Parameters.AddWithValue("@in_notificationsEnabled", us.NotificationsEnabled);
            command.Parameters.AddWithValue("@in_mfaEnabled", us.MfaEnabled);
            command.Parameters.AddWithValue("@in_isTenantUser", us.IsTenantUser);
            command.Parameters.AddWithValue("@in_isAdminUser", us.IsAdminUser);
            command.Parameters.AddWithValue("@in_FirstName", us.FirstName);
            command.Parameters.AddWithValue("@in_LastName", us.LastName);
            command.Parameters.AddWithValue("@in_Roles", us.Roles);

        }
        public async Task<bool> DeleteUser(string UserId)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_userDelete";
            command.Parameters.AddWithValue("@in_userId", UserId);
            command.CommandType = CommandType.StoredProcedure;

            User? getuser = await GetuserAsync(UserId);
            if (getuser != null)
            {
                APIStatus ?status = new APIStatus();
                List<KeyClient?> res = await keycloak.GetUser(getuser.Email);
                if (res == null)
                {
                    status.Code = "204";
                    status.Message = "email id not found in AUTH!";
                }
                else
                {
                    status = await keycloak.DeleteUser(res?.FirstOrDefault()?.id, UserId);
                }
            }
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }

        public async Task<User?> GetuserusingUserNameAsync(string? UserName)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_GetUserByUserName";
            command.Parameters.AddWithValue("@p_userName", UserName);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        public async Task<List<User?>> GetTenantUserList(int tenantId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.Parameters.AddWithValue("@in_tenantId", tenantId);
            command.CommandText = @"USP_GetTenantUsers";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllUsersAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<APIStatus> Forgetpassword(string emailid)
        {
            APIStatus ?status = new APIStatus();
            User? user = await GetuserusingUserNameAsync(emailid);
            if (user == null)
            {
                status.Code = "204";
                status.Message = "email id not found!";
            }
            else
            {
                KeyClientOpr opr = new KeyClientOpr();
                List<KeyClient?> client = await keycloak.GetUser(emailid); //await opr.KeyClockClientGet(emailid);
                if (client == null)
                {
                    status.Code = "204";
                    status.Message = "email id not found in AUTH!";
                }
                else
                {
                    status = await keycloak.ResetPassword(client.FirstOrDefault()?.id, emailid);  //await opr.ResetPassword(client.FirstOrDefault()?.id, emailid);
                }
            }
            return status;
        }
        public async Task<APIStatus> verifyemail(string emailid)
        {
            APIStatus ?status = new APIStatus();
            User? user = await GetuserusingUserNameAsync(emailid);
            if (user == null)
            {
                status.Code = "204";
                status.Message = "email id not found!";
            }
            else
            {
                List<KeyClient?> res = await keycloak.GetUser(emailid);
                if (res == null)
                {
                    status.Code = "204";
                    status.Message = "email id not found in AUTH!";
                }
                else
                {
                    status = await keycloak.Verifyemail(res?.FirstOrDefault()?.id, emailid);
                }
            }
            return status;
        }
        private async Task<IReadOnlyList<User>> ReadAllUsersAsync(MySqlDataReader reader)
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
                        NotificationsEnabled = reader.IsDBNull(11) ? false : (reader.GetInt16(11) == 1),
                        MfaEnabled = reader.IsDBNull(12) ? false : (reader.GetInt16(12) == 1),
                        IsTenantUser = reader.IsDBNull(13) ? 0 : reader.GetInt32(13),
                        IsAdminUser = reader.IsDBNull(14) ? 0 : reader.GetInt32(14),
                        FirstName = reader.IsDBNull(15) ? null : reader.GetValue(15).ToString(),
                        LastName = reader.IsDBNull(16) ? null : reader.GetValue(16).ToString(),
                        Roles = reader.IsDBNull(17) ? null : reader.GetValue(17).ToString(),
                    };
                    users.Add(user);
                }

            }
            return users;
        }

    }
}
