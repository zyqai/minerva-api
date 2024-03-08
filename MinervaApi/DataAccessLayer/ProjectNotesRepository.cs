using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;
using MySqlConnector;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Minerva.DataAccessLayer
{
    public class ProjectNotesRepository: IProjectNotesRepository
    {
        MySqlDataSource database;
        public ProjectNotesRepository(MySqlDataSource _dataSource)
        {
            database = _dataSource;
        }

        private void AddParameters(MySqlCommand command, ProjectNotes dt)
        {
            command.Parameters.AddWithValue("@p_projectId", dt.ProjectId);
            command.Parameters.AddWithValue("@p_tenantId", dt.TenantId);
            command.Parameters.AddWithValue("@p_notes", dt.Notes);
            command.Parameters.AddWithValue("@p_createdByUserId", dt.CreatedByUserId);

        }

        public async Task<int> SaveProjectNotes(ProjectNotes dt)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_ProjectNotesCreate";
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


        public async Task<ProjectNotes?> GetProjectNotesAsync(int? ProjectNotesId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_ProjectNotesGetById";
            command.Parameters.AddWithValue("@p_ProjectNotesId", ProjectNotesId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<List<ProjectNotes>> ReadAllAsync(MySqlDataReader reader)
        {
            var ProjectNotes = new List<ProjectNotes>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var dt = new ProjectNotes
                    {
                        ProjectNotesId = Convert.ToInt32(reader["projectNotesId"]),
                        ProjectId = reader["projectId"] == DBNull.Value ? -1 : Convert.ToInt32(reader["projectId"]),
                        TenantId = reader["tenantId"] == DBNull.Value ? -1 : Convert.ToInt32(reader["tenantId"]),
                        Notes = reader["notes"] == DBNull.Value ? "" : reader["notes"].ToString(),
                        CreatedByUserId = reader["createdByUserId"] == DBNull.Value ? "" : reader["createdByUserId"].ToString(),
                        CreatedOn = Convert.ToDateTime(reader["createdOn"])

                    };
                    ProjectNotes.Add(dt);
                }

            }
            return ProjectNotes;
        }
        public async Task<ProjectNotesResponse?> GetALLProjectNotessAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_ProjectNotesGetAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            //return [.. result];

            ProjectNotesResponse? ft = new ProjectNotesResponse();

            if (result != null)
            {
                ft.code = "206";
                ft.message = "Response available";
                ft.ProjectNotes = result;
            }
            else
            {
                ft.code = "204";
                ft.message = "No Content";
            }

            return ft;
        }

        public async Task<bool> UpdateProjectNotes(ProjectNotes dt)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = "usp_ProjectNotesUpdate";
                command.Parameters.AddWithValue("@p_ProjectNotesId", dt.ProjectNotesId);
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

        public async Task<bool> DeleteProjectNotes(int? ProjectNotesId)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_ProjectNotesDelete";
                command.Parameters.AddWithValue("@p_projectNotesId", ProjectNotesId);
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
