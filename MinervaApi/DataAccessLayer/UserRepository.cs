﻿using Minerva.Models;
using Minerva.IDataAccessLayer;
using MySqlConnector;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Data;
using System.Collections.Generic;
using MinervaApi.ExternalApi;

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
            int i =await command.ExecuteNonQueryAsync();
            string? lastInsertId = outputParameter.Value != null ? outputParameter.Value.ToString() : "";
            connection.Close();
            if (i > 0)
            {
                KeyClientCrud crd = new KeyClientCrud();
                KeyClient client = new KeyClient();
                client.id = "";
                client.email = us.Email;
                client.emailVerified = false;
                client.username = us.Email;
                client.firstName = us.Email;
                client.lastName = us.UserName;
                client.enabled = us.IsActive;
                client.realmRoles = [];
                client.requiredActions = ["UPDATE_PASSWORD", "VERIFY_EMAIL"];
                var res=await crd.ClientInsert(client);
                return lastInsertId;
            }
            return string.Empty;

        }
        public async Task<bool> UpdateUser(User us)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_UpdateUser";
            AddUserParameters(command, us);
            command.Parameters.AddWithValue("@p_modifiedBy", us.ModifiedBy);
            command.CommandType = CommandType.StoredProcedure;
            int i =await command.ExecuteNonQueryAsync();
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
        }
        public async Task<bool> DeleteUser(string UserId)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_DeleteUser";
            command.Parameters.AddWithValue("@in_userId", UserId);
            command.CommandType = CommandType.StoredProcedure;
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
    }
}
