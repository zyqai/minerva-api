using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace MinervaApi.DataAccessLayer
{
    public class ProjectRepository : IProjectRepository
    {
        MySqlDataSource database;
        public ProjectRepository(MySqlDataSource mySql)
        {
            this.database = mySql;
        }

        public async Task<int> SaveProject(Project p)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "USP_ProjectCreate";
                AddParameters(command, p);
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
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void AddParameters(MySqlCommand command, Project p)
        {
            command.Parameters.AddWithValue("@in_tenantId", p.TenantId);
            command.Parameters.AddWithValue("@in_projectName", p.ProjectName);
            command.Parameters.AddWithValue("@in_projectDescription", p.ProjectDescription);
            command.Parameters.AddWithValue("@in_industryId", p.IndustryId);
            command.Parameters.AddWithValue("@in_amount", p.Amount);
            command.Parameters.AddWithValue("@in_purpose", p.Purpose);
            command.Parameters.AddWithValue("@in_createdByUserId", p.CreatedByUserId);
            command.Parameters.AddWithValue("@in_assignedToUserId", p.AssignedToUserId);
            command.Parameters.AddWithValue("@in_loanTypeAutoId", p.LoanTypeAutoId);
            command.Parameters.AddWithValue("@in_statusAutoId", p.StatusAutoId);
            command.Parameters.AddWithValue("@in_projectFilesPath", p.ProjectFilesPath);
        }

        public async Task<Project?> GetProjectAsync(int Id_Projects)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_GetProjectById";
            command.Parameters.AddWithValue("@in_projectId", Id_Projects);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<IReadOnlyList<Project>> ReadAllAsync(MySqlDataReader reader)
        {
            var bu = new List<Project>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new Project
                    {
                        ProjectId = reader["ProjectId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ProjectId"]),
                        TenantId = reader["TenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TenantId"]),
                        ProjectName = reader["ProjectName"] == DBNull.Value ? string.Empty : reader["ProjectName"].ToString(),
                        ProjectDescription = reader["ProjectDescription"] == DBNull.Value ? string.Empty : reader["ProjectDescription"].ToString(),
                        IndustryId = reader["industryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["industryId"]),
                        Amount = reader["Amount"] == DBNull.Value ? string.Empty : reader["Amount"].ToString(),
                        Purpose = reader["purpose"] == DBNull.Value ? string.Empty : reader["purpose"].ToString(),
                        CreatedByUserId = reader["CreatedByUserId"] == DBNull.Value ? string.Empty : reader["CreatedByUserId"].ToString(),
                        CreatedDateTime = reader["createdDateTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["createdDateTime"].ToString()),
                        AssignedToUserId = reader["assignedToUserId"] == DBNull.Value ? string.Empty : reader["assignedToUserId"].ToString(),
                        ModifiedByUserId = reader["modifiedByUserId"] == DBNull.Value ? string.Empty : reader["modifiedByUserId"].ToString(),
                        ModifiedDateTime = reader["modifiedDateTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["modifiedDateTime"].ToString()),
                        LoanTypeAutoId = reader["loanTypeAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["loanTypeAutoId"]),
                        StatusAutoId = reader["statusAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["statusAutoId"]),
                        ProjectFilesPath = reader["projectFilesPath"] == DBNull.Value ? string.Empty : reader["projectFilesPath"].ToString(),
                    };
                    bu.Add(user);
                }
            }
            return bu;
        }

        public async Task<List<Project?>> GetAllProjectAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ProjectGetAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<bool> UpdateProject(Project p)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = "USP_ProjectUpdate";
                command.Parameters.AddWithValue("@in_projectId", p.ProjectId);
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

        public async Task<bool> DeleteProject(int Id_Projects)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "USP_DeleteProject";
                command.Parameters.AddWithValue("@in_projectId", Id_Projects);
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

        public async Task<List<Project>?> GetProjectByTenantAsync(int tenantId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_TenentProjects";
            command.Parameters.AddWithValue("@p_tenantId", tenantId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.ToList();
        }
    }
}
