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
            command.Parameters.AddWithValue("@p_requestTemplateId", dt.RequestTemplateDetailsId);
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
                command.CommandType = CommandType.StoredProcedure;
                int rowsAffected = await command.ExecuteNonQueryAsync();
                
                return rowsAffected;

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

        private async Task<List<RequestTemplateDetails>> ReadAllAsync(MySqlDataReader reader)
        {
            var RequestTemplateDetailss = new List<RequestTemplateDetails>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var dt = new RequestTemplateDetails
                    {
                        RequestTemplateDetailsId = Convert.ToInt32(reader["requestTemplateDetailsId"]),
                        RequestTemplateId = Convert.ToInt32(reader["requestTemplateId"]),
                        TenantId = Convert.ToInt32(reader["tenantId"]),
                        Label = reader["label"].ToString(),
                        DocumentTypeAutoId = Convert.ToInt32(reader["documentTypeAutoId"]),

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
