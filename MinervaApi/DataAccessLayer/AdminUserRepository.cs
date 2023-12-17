using Microsoft.AspNetCore.Mvc;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data.Common;

namespace Minerva.DataAccessLayer
{
    public class AdminUserRepository : IAdminUserRepository
    {
        MySqlDataSource database;
        public AdminUserRepository(MySqlDataSource _database)
        {
            this.database = _database;
        }
        public async Task<AdminUser?> FindOneAsync(int id)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT `username`, `password`, `isAdmin`, `isActive`, `createdBy`, `modifiedBy`, `createTime`,`modifiedTime` FROM `minerva`.`_adminusers` WHERE `adminUserId` = @id;";
            command.Parameters.AddWithValue("@id", id);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        private async Task<IReadOnlyList<AdminUser>> ReadAllAsync(DbDataReader reader)
        {
            var users = new List<AdminUser>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new AdminUser
                    {
                        UserName = reader.GetValue(0).ToString(),
                        Password = reader.GetValue(1).ToString(),
                        IsAdmin = reader.GetInt16(2) == 1 ? true : false,
                        IsActive = reader.GetInt16(3) == 1 ? true : false,
                        CreatedBy = reader.GetValue(4).ToString(),
                        ModifiedBy = reader.GetValue(5).ToString(),
                        CreateTime = reader.GetDateTime(6),
                        ModifiedTime = reader.GetDateTime(7)
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public async Task<AdminUser?> FindUserPassword(string username, string password)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT `username`, `password`, `isAdmin`, `isActive`, `createdBy`, `modifiedBy`, `createTime`,`modifiedTime` FROM `minerva`.`_adminusers` WHERE `username` = @username and  `password` = @password;";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }
    }
}