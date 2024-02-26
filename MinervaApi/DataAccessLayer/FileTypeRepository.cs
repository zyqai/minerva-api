using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;

namespace Minerva.DataAccessLayer
{
    public class FileTypeRepository : IFileTypeRepository
    {
        MySqlDataSource database;
        public FileTypeRepository(MySqlDataSource _dataSource)
        {
            database = _dataSource;
        }

        private void AddParameters(MySqlCommand command, FileType ft)
        {
           command.Parameters.AddWithValue("@p_fileTypeId", ft.FileTypeId);
            command.Parameters.AddWithValue("@p_tenantId", ft.TenantId);
            command.Parameters.AddWithValue("@p_fileTypeName", ft.FileTypeName);
        }

        public async Task<bool> SaveFileType(FileType p)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_fileTypeCreate";
                AddParameters(command, p);
                command.CommandType = CommandType.StoredProcedure;
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        public async Task<FileType?> GetFileTypeAsync(int? FileTypeAutoId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_fileTypeGetById";
            command.Parameters.AddWithValue("@p_filetypeAutoId", FileTypeAutoId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<IReadOnlyList<FileType>> ReadAllAsync(MySqlDataReader reader)
        {
            var filetypes = new List<FileType>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var ft = new FileType
                    {
                        FileTypeAutoId = Convert.ToInt32(reader["fileTypeAutoId"]),
                        FileTypeId = Convert.ToInt32(reader["fileTypeId"]),
                        TenantId = Convert.ToInt32(reader["tenantId"]),
                        FileTypeName = reader["fileTypeName"].ToString()
                    };
                    filetypes.Add(ft);
                }

            }
            return filetypes;
        }
        public async Task<List<FileType?>> GetALLFileTypesAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_filetypesGetAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<bool> UpdateFileType(FileType ft)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = "usp_filetypeUpdate";
                command.Parameters.AddWithValue("@p_fileTypeAutoId", ft.FileTypeAutoId);
                AddParameters(command, ft);
                command.CommandType = CommandType.StoredProcedure;
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public async Task<bool> DeleteFileType(int? FileTypeAutoId)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_filetypeDelete";
                command.Parameters.AddWithValue("@fileTypeAutoId", FileTypeAutoId);
                command.CommandType = CommandType.StoredProcedure;
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


    }
}
