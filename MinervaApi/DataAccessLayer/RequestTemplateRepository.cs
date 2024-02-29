using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;

namespace Minerva.DataAccessLayer
{
    public class RequestTemplateRepository: IRequestTemplateRepository
    {

        MySqlDataSource database;
        public RequestTemplateRepository(MySqlDataSource _dataSource)
        {
            database = _dataSource;
        }

        private void AddParameters(MySqlCommand command, RequestTemplate dt)
        {
            
            command.Parameters.AddWithValue("@p_tenantId", dt.tenantId);
            command.Parameters.AddWithValue("@p_RequestTemplateName", dt.requestTemplateName);
            command.Parameters.AddWithValue("@p_RequestTemplateDescription", dt.requestTemplateDescription);
            command.Parameters.AddWithValue("@p_remindersAutoId", dt.remindersAutoId);

        }

        public async Task<int> SaveRequestTemplate(RequestTemplate dt)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_RequestTemplateCreate";
                AddParameters(command, dt);
                command.CommandType = CommandType.StoredProcedure;
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        public async Task<RequestTemplate?> GetRequestTemplateAsync(int? requestTemplateId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_RequestTemplateGetById";
            command.Parameters.AddWithValue("@p_RequestTemplateId", requestTemplateId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<IReadOnlyList<RequestTemplate>> ReadAllAsync(MySqlDataReader reader)
        {
            var RequestTemplates = new List<RequestTemplate>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var dt = new RequestTemplate
                    {
                        requestTemplateId = Convert.ToInt32(reader["requestTemplateId"]),
                        tenantId = Convert.ToInt32(reader["tenantId"]),
                        requestTemplateName = reader["requestTemplateName"].ToString(),
                        requestTemplateDescription = reader["requestTemplateDescription"].ToString(),
                        remindersAutoId = Convert.ToInt32(reader["remindersAutoId"]),

                    };
                    RequestTemplates.Add(dt);
                }

            }
            return RequestTemplates;
        }
        public async Task<List<RequestTemplate?>> GetALLRequestTemplatesAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_RequestTemplatesGetAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<bool> UpdateRequestTemplate(RequestTemplate dt)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = "usp_RequestTemplateUpdate";
                command.Parameters.AddWithValue("@p_requestTemplateId", dt.requestTemplateId);
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

        public async Task<bool> DeleteRequestTemplate(int? RequestTemplateAutoId)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_RequestTemplateDelete";
                command.Parameters.AddWithValue("@RequestTemplateAutoId", RequestTemplateAutoId);
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
