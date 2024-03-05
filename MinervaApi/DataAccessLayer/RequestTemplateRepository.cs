using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Returns;
using MySqlConnector;
using Newtonsoft.Json;
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

        public async Task<Apistatus> SaveRequestTemplate(RequestTemplateRequestWhithDetails dt)
        {
            Apistatus apistatus = new Apistatus();
            MySqlConnection connection = null;
            try
            {
                using (connection = new MySqlConnection())
                {
                    connection = database.OpenConnection();
                    using (MySqlCommand command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_RequestTemplateWithDetailsInsert";
                        // Serialize the request object to a JSON string
                        string RequestTemplateDetails = JsonConvert.SerializeObject(dt.RequestTemplateDetails);

                        command.Parameters.AddWithValue("@in_tenantId", dt.TenantId);
                        command.Parameters.AddWithValue("@in_requestTemplateName", dt.RequestTemplateName);
                        command.Parameters.AddWithValue("@in_requestTemplateDescription", dt.RequestTemplateDescription);
                        command.Parameters.AddWithValue("@in_remindersAutoId", dt.RemindersAutoId);
                        command.Parameters.AddWithValue("@in_requestTemplateDetails", RequestTemplateDetails);

                        MySqlParameter outputParameter = new MySqlParameter("@out_message", MySqlDbType.VarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParameter);

                        command.ExecuteNonQuery();
                        // Get the response message from the stored procedure
                        string message = command.Parameters["@out_message"].Value.ToString();

                        apistatus.message= message;
                        apistatus.code = message == "Insertion successful." ? "200" : "500"; 
                    }
                }
            }
            catch (Exception ex)
            {
                apistatus.message = ex.Message;
                apistatus.code = "500";
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return apistatus;
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

        private async Task<List<RequestTemplate>> ReadAllAsync(MySqlDataReader reader)
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
        public async Task<RequestTemplateResponse?> GetALLRequestTemplatesAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_RequestTemplatesGetAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();


            RequestTemplateResponse? ft = new RequestTemplateResponse();

            if (result != null)
            {
                ft.code = "206";
                ft.message = "Response available";
                ft.RequestTemplates = result;
            }
            else
            {
                ft.code = "204";
                ft.message = "No Content";
            }

            return ft;

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
