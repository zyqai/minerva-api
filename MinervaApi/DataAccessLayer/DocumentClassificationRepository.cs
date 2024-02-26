using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;

namespace Minerva.DataAccessLayer
{
    public class DocumentClassificationRepository:IDocumentClassificationRepository
    {
        MySqlDataSource database;
        public DocumentClassificationRepository(MySqlDataSource _dataSource)
        {
            database = _dataSource;
        }

        private void AddParameters(MySqlCommand command, DocumentClassification ft)
        {
            command.Parameters.AddWithValue("@p_DocumentClassificationId", ft.DocumentClassificationId);
            command.Parameters.AddWithValue("@p_tenantId", ft.TenantId);
            command.Parameters.AddWithValue("@p_DocumentClassificationName", ft.DocumentClassificationName);
        }

        public async Task<bool> SaveDocumentClassification(DocumentClassification p)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_DocumentClassificationCreate";
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


        public async Task<DocumentClassification?> GetDocumentClassificationAsync(int? DocumentClassificationAutoId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_DocumentClassificationGetById";
            command.Parameters.AddWithValue("@p_DocumentClassificationAutoId", DocumentClassificationAutoId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<IReadOnlyList<DocumentClassification>> ReadAllAsync(MySqlDataReader reader)
        {
            var DocumentClassifications = new List<DocumentClassification>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var ft = new DocumentClassification
                    {
                        DocumentClassificationAutoId = Convert.ToInt32(reader["DocumentClassificationAutoId"]),
                        DocumentClassificationId = Convert.ToInt32(reader["DocumentClassificationId"]),
                        TenantId = Convert.ToInt32(reader["tenantId"]),
                        DocumentClassificationName = reader["DocumentClassificationName"].ToString()
                    };
                    DocumentClassifications.Add(ft);
                }

            }
            return DocumentClassifications;
        }
        public async Task<List<DocumentClassification?>> GetALLDocumentClassificationsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_DocumentClassificationsGetAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<bool> UpdateDocumentClassification(DocumentClassification ft)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = "usp_DocumentClassificationUpdate";
                command.Parameters.AddWithValue("@p_DocumentClassificationAutoId", ft.DocumentClassificationAutoId);
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

        public async Task<bool> DeleteDocumentClassification(int? DocumentClassificationAutoId)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_DocumentClassificationDelete";
                command.Parameters.AddWithValue("@DocumentClassificationAutoId", DocumentClassificationAutoId);
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
