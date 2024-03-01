using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;

namespace Minerva.DataAccessLayer
{
    public class DocumentTypeRepository:IDocumentTypeRepository
    {
        MySqlDataSource database;
        public DocumentTypeRepository(MySqlDataSource _dataSource)
        {
            database = _dataSource;
        }

        private void AddParameters(MySqlCommand command, DocumentType dt)
        {
            command.Parameters.AddWithValue("@p_DocumentTypeId", dt.DocumentTypeId);
            command.Parameters.AddWithValue("@p_tenantId", dt.TenantId);
            command.Parameters.AddWithValue("@p_DocumentTypeName", dt.DocumentTypeName);
            command.Parameters.AddWithValue("@p_documentTypeDescription", dt.DocumentTypeDescription);
            command.Parameters.AddWithValue("@p_documentClassificationId", dt.DocumentClassificationId);
            command.Parameters.AddWithValue("@p_templateFilePath", dt.TemplateFilePath);


        }

        public async Task<bool> SaveDocumentType(DocumentType dt)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_DocumentTypeCreate";
                AddParameters(command, dt);
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


        public async Task<DocumentType?> GetDocumentTypeAsync(int? DocumentTypeAutoId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_DocumentTypeGetById";
            command.Parameters.AddWithValue("@p_DocumentTypeAutoId", DocumentTypeAutoId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<List<DocumentType>> ReadAllAsync(MySqlDataReader reader)
        {
            var DocumentTypes = new List<DocumentType>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var dt = new DocumentType
                    {
                        DocumentTypeAutoId = Convert.ToInt32(reader["DocumentTypeAutoId"]),
                        DocumentTypeId = Convert.ToInt32(reader["DocumentTypeId"]),
                        TenantId = Convert.ToInt32(reader["tenantId"]),
                        DocumentTypeName = reader["DocumentTypeName"].ToString(),
                        DocumentTypeDescription = reader["documentTypeDescription"].ToString(),
                        DocumentClassificationId = Convert.ToInt32(reader["documentClassificationId"]),
                        TemplateFilePath = reader["templateFilePath"].ToString()

                    };
                    DocumentTypes.Add(dt);
                }

            }
            return DocumentTypes;
        }
        public async Task<DocumentTypeResponse?> GetALLDocumentTypesAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_DocumentTypesGetAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            //return [.. result];

            DocumentTypeResponse? ft = new DocumentTypeResponse();

            if (result != null)
            {
                ft.code = "206";
                ft.message = "Response available";
                ft.DocumentTypes = result;
            }
            else
            {
                ft.code = "204";
                ft.message = "No Content";
            }

            return ft;
        }

        public async Task<bool> UpdateDocumentType(DocumentType dt)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = "usp_DocumentTypeUpdate";
                command.Parameters.AddWithValue("@p_DocumentTypeAutoId", dt.DocumentTypeAutoId);
                AddParameters(command, dt);
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

        public async Task<bool> DeleteDocumentType(int? DocumentTypeAutoId)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_DocumentTypeDelete";
                command.Parameters.AddWithValue("@DocumentTypeAutoId", DocumentTypeAutoId);
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
