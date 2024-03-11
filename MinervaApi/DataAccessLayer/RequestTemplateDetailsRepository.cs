using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;
using System.Reflection.Emit;


namespace Minerva.DataAccessLayer
{
    public class RequestTemplateDetailsRepository: IRequestTemplateDetailsRepository
    {
        MySqlDataSource database;
        public RequestTemplateDetailsRepository(MySqlDataSource _dataSource)
        {
            database = _dataSource;
        }

        private void AddParameters(MySqlCommand command, RequestTemplateDetails dt)
        {
            command.Parameters.AddWithValue("@p_requestTemplateId", dt.RequestTemplateId);
            command.Parameters.AddWithValue("@p_tenantId", dt.TenantId);
            command.Parameters.AddWithValue("@p_label", dt.Label);
            command.Parameters.AddWithValue("@p_documentTypeAutoId", dt.DocumentTypeAutoId);


        }

        public async Task<int> SaveRequestTemplateDetails(RequestTemplateDetails dt)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_RequestTemplateDetailsCreate";
                AddParameters(command, dt);
                MySqlParameter outputParameter = new MySqlParameter("@p_last_insert_id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParameter);
                command.CommandType = CommandType.StoredProcedure;
                int rowsAffected = await command.ExecuteNonQueryAsync();
                int lastInsertId = Convert.ToInt32(outputParameter.Value);
                if (rowsAffected == 1)
                {
                    rowsAffected = lastInsertId;
                }
                return rowsAffected;

            }
            catch (Exception)
            {
                throw;
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


        public async Task<RequestTemplateDetails?> GetRequestTemplateDetailsAsync(int? requestTemplateDetailsId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_RequestTemplateDetailsGetById";
            command.Parameters.AddWithValue("@p_requestTemplateDetailsId", requestTemplateDetailsId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        public async Task<List<RequestTemplateDetails?>?> GetRequestTemplateDetailsByTemplateIdAsync(int? requestTemplateId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_requestTemplateDetailsByTemplateId";
            command.Parameters.AddWithValue("@p_requestTemplateId", requestTemplateId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result;
        }

        private async Task<List<RequestTemplateDetails>> ReadAllAsync(MySqlDataReader reader)
        {
            var RequestTemplateDetailss = new List<RequestTemplateDetails>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var dt = new RequestTemplateDetails
                    {
                        RequestTemplateDetailsId = reader.IsDBNull("TenantId") ? null : Convert.ToInt32(reader["requestTemplateDetailsId"]),
                        RequestTemplateId = reader.IsDBNull("TenantId") ? null : Convert.ToInt32(reader["requestTemplateId"]),
                        TenantId = reader.IsDBNull("TenantId") ? null : Convert.ToInt32(reader["tenantId"]),
                        Label = reader.IsDBNull("label") ? null : reader["label"].ToString(),
                        DocumentTypeAutoId = reader.IsDBNull("documentTypeAutoId") ? null : (int?)reader.GetInt32("documentTypeAutoId"),

                    };
                    RequestTemplateDetailss.Add(dt);
                }

            }
            return RequestTemplateDetailss;
        }
        public async Task<RequestTemplateDetailsResponse?> GetALLRequestTemplateDetailssAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_RequestTemplateDetailsGetAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            List< RequestTemplateDetails> result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();

            RequestTemplateDetailsResponse? ft = new RequestTemplateDetailsResponse();

            if (result != null)
            {
                ft.code = "206";
                ft.message = "Response available";
                ft.RequestTemplateDetails = result;
            }
            else
            {
                ft.code = "204";
                ft.message = "No Content";
            }

            return ft;

        }

        public async Task<bool> UpdateRequestTemplateDetails(RequestTemplateDetails dt)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = "usp_RequestTemplateDetailsUpdate";
                command.Parameters.AddWithValue("@p_requestTemplateDetailsId", dt.RequestTemplateDetailsId);
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

        public async Task<bool> DeleteRequestTemplateDetails(int? requestTemplateDetailsId)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_RequestTemplateDetailsDelete";
                command.Parameters.AddWithValue("@requestTemplateDetailsId", requestTemplateDetailsId);
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
