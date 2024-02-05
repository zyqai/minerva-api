﻿using Minerva.Models;
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
                        NotificationsEnabled = reader.IsDBNull(11) ? false : (reader.GetInt16(11) == 1),
                        MfaEnabled = reader.IsDBNull(12) ? false : (reader.GetInt16(12) == 1),
                        IsTenantUser = reader.IsDBNull(13) ? 0 : reader.GetInt32(13),
                        IsAdminUser = reader.IsDBNull(14) ? 0 : reader.GetInt32(14),
                        FirstName = reader.IsDBNull(15) ? null : reader.GetValue(15).ToString(),
                        LastName = reader.IsDBNull(16) ? null : reader.GetValue(16).ToString(),
                        Roles = reader.IsDBNull(17) ? null : reader.GetValue(17).ToString(),


                    };
                    users.Add(user);
                    if (user.TenantId != 0)
                    {
                        user.Tenant = new Tenant
                        {
                            TenantId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18),
                            TenantName = reader.GetString(19),
                            TenantDomain = reader.GetString(20),
                            TenantLogoPath = reader.GetString(21),
                            TenantAddress = reader.GetString(22),
                            TenantAddress1 = reader.GetString(23),
                            TenantPhone = reader.GetString(24),
                            TenantContactName = reader.GetString(25),
                            TenantContactEmail = reader.GetString(26),
                            PostalCode = reader.GetString(27),
                            City = reader.GetString(28),
                            stateid = reader.GetInt16(29),
                        };
                    }
                }

            }
            return users;
        }
        public async Task<string> SaveUser(User us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_CreateUser";
            AddUserParameters(command, us);
            command.Parameters.AddWithValue("@p_createdBy", us.CreatedBy);
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
                var res =keycloak.CreateUser(us);
                //KeyClientOpr crd = new KeyClientOpr();
                //KeyClient client = new KeyClient();
                //client.id = "";
                //client.email = us.Email;
                //client.emailVerified = false;
                //client.username = us.Email;
                //client.firstName = us.FirstName;
                //client.lastName = us.LastName;
                ////client.realmRoles = [us.Roles];
                //client.enabled = us.IsActive;
                ////client.realmRoles = [];
                //client.requiredActions = ["UPDATE_PASSWORD", "VERIFY_EMAIL"];
                //var res = await crd.ClientInsert(client);
                //List<KeyClient?> clientDetails = await crd.KeyClockClientGet(us.Email);
                //APIStatus status = new APIStatus();
                //status = await crd.sendverifyemail(clientDetails.FirstOrDefault()?.id, us.Email);
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
                command.Parameters.AddWithValue("@p_userId", us.UserId);
            }
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
            command.Parameters.AddWithValue("@P_FirstName", us.FirstName);
            command.Parameters.AddWithValue("@P_LastName", us.LastName);
            command.Parameters.AddWithValue("@P_Roles", us.Roles);

        }
        public async Task<bool> DeleteUser(string UserId)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_UserDelete";
            command.Parameters.AddWithValue("@in_userId", UserId);
            command.CommandType = CommandType.StoredProcedure;

            User? getuser = await GetuserAsync(UserId);
            if (getuser != null)
            {
                APIStatus ?status = new APIStatus();
               //List<KeyClient?> res = await keycloak.GetUser("santhoshdonthi@gmail.com");
                List<KeyClient?> res = await keycloak.GetUser(UserId);
                if (res == null)
                {
                    status.Code = "204";
                    status.Message = "email id not found in AUTH!";
                }
                else
                {
                   // status = await keycloak.DeleteUser(res?.FirstOrDefault()?.id, "santhoshdonthi@gmail.com");
                    status = await keycloak.DeleteUser(res?.FirstOrDefault()?.id, UserId);
                }
                //KeyClientOpr opr = new KeyClientOpr();
                //List<KeyClient?> client = await opr.KeyClockClientGet(getuser?.Email);
                //if (client == null)
                //{
                //    status.Code = "204";
                //    status.Message = "email id not found in AUTH!";
                //    throw new Exception(status.Message);
                //}
                //else
                //{
                //    status = await opr.keyclockclientDelete(client.FirstOrDefault()?.id, getuser.Email);
                //}
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
            command.Parameters.AddWithValue("@p_tenantId", tenantId);
            command.CommandText = @"USP_GetTenantUsers";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
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
                List<KeyClient?> res = await keycloak.GetUser("santhoshdonthi@gmail.com");
                if (res == null)
                {
                    status.Code = "204";
                    status.Message = "email id not found in AUTH!";
                }
                else
                {
                    status = await keycloak.ResetPassword(res?.FirstOrDefault()?.id,"santhoshdonthi@gmail.com");
                }

                //KeyClientOpr opr = new KeyClientOpr();
                //List<KeyClient> client = await opr.KeyClockClientGet(emailid);
                //if (client == null)
                //{
                //    status.Code = "204";
                //    status.Message = "email id not found in AUTH!";
                //}
                //else
                //{
                //    status=await opr.ResetPassword(client.FirstOrDefault()?.id, emailid);
                //}
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
                //List<KeyClient?> res = await keycloak.GetUser("santhoshdonthi@gmail.com");
                List<KeyClient?> res = await keycloak.GetUser(emailid);
                if (res == null)
                {
                    status.Code = "204";
                    status.Message = "email id not found in AUTH!";
                }
                else
                {
                    status = await keycloak.Verifyemail(res?.FirstOrDefault()?.id, emailid);
                    //status = await keycloak.Verifyemail(res?.FirstOrDefault()?.id, "santhoshdonthi@gmail.com");
                }

                //KeyClientOpr opr = new KeyClientOpr();
                //List<KeyClient> client = await opr.KeyClockClientGet(emailid);
                //if (client == null)
                //{
                //    status.Code = "204";
                //    status.Message = "email id not found in AUTH!";
                //}
                //else
                //{
                //    status = await opr.sendverifyemail(client.FirstOrDefault()?.id, emailid);
                //}
            }
            return status;
        }
    }

}
