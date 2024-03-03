using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Returns;
using MinervaApi.Models.Requests;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<Project?> GetProjectAsync(int? Id_Projects)
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
                        TenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
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

        public async Task<int> SaveProjectWithDetails(ProjectwithDetailsRequest request, User? user, int? peopleId, int? personaAutoId, int? businessId)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            try
            {
                command.CommandText = "Usp_InsertProjectData";


                command.Parameters.AddWithValue("@in_ProjectName", request.ProjectName);
                command.Parameters.AddWithValue("@in_ProjectDescription", request.ProjectDescription);
                command.Parameters.AddWithValue("@in_IndustryId", request.IndustryId);
                command.Parameters.AddWithValue("@in_Amount", request.Amount);
                command.Parameters.AddWithValue("@in_Purpose", request.Purpose);
                command.Parameters.AddWithValue("@in_AssignedToUserId", request.AssignedToUserId);
                command.Parameters.AddWithValue("@in_LoanTypeAutoId", request.LoanTypeAutoId);
                command.Parameters.AddWithValue("@in_StatusAutoId", request.StatusAutoId);
                command.Parameters.AddWithValue("@in_ProjectFilesPath", request.ProjectFilesPath);
                command.Parameters.AddWithValue("@in_Notes", request.Notes);
                command.Parameters.AddWithValue("@in_PrimaryBorrower", request.PrimaryBorrower);
                command.Parameters.AddWithValue("@in_PrimaryBusiness", request.PrimaryBusiness);


                command.Parameters.AddWithValue("@in_CreatedBy", user?.UserId);
                command.Parameters.AddWithValue("@in_TenantId", user?.TenantId);
                command.Parameters.AddWithValue("@in_peopleId", peopleId);// request.projectPeopleRelations.Where(w=>w.primaryBorrower==1).Select(s=>s.peopleId));
                command.Parameters.AddWithValue("@in_personaAutoId", personaAutoId);// request.projectPeopleRelations.Where(w=>w.primaryBorrower==1).Select(s=>s.personaAutoId));
                command.Parameters.AddWithValue("@in_businessId", businessId);// request.projectPeopleRelations.Where(w=>w.primaryBorrower==1).Select(s=>s.personaAutoId));

                MySqlParameter param = new MySqlParameter("@in_ProjectStartDate", MySqlDbType.DateTime);
                param.Value = request.ProjectStartDate;
                command.Parameters.Add(param);

                MySqlParameter param1 = new MySqlParameter("@in_DesiredClosedDate", MySqlDbType.DateTime);
                param.Value = request.DesiredClosedDate;
                command.Parameters.Add(param1);

                MySqlParameter outputParameter = new MySqlParameter("@p_last_ProjectNameinsert_id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParameter);
                command.CommandType = CommandType.StoredProcedure;
                int rowsAffected = await command.ExecuteNonQueryAsync();
                int lastInsertId = Convert.ToInt32(outputParameter.Value);
                if (lastInsertId >= 1)
                {
                    rowsAffected = lastInsertId;
                }
                return rowsAffected;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message; // This will contain the MySQL error message

                // Read the custom error message raised in the stored procedure
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

        public async Task<List<ProjectDetails>> GetAllProjectsWithDetails(int? tenantId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.Parameters.AddWithValue("@in_tenantid", tenantId);
            command.CommandText = @"usp_ProjectListWithDetails";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllProjectsWithDetailsAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        private async Task<IReadOnlyList<ProjectDetails>> ReadAllProjectsWithDetailsAsync(MySqlDataReader reader)
        {
            var bu = new List<ProjectDetails>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new ProjectDetails
                    {
                        projectId = reader["ProjectId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ProjectId"]),
                        tenantId = reader["TenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TenantId"]),
                        projectName = reader["ProjectName"] == DBNull.Value ? string.Empty : reader["ProjectName"].ToString(),
                        projectDescription = reader["ProjectDescription"] == DBNull.Value ? string.Empty : reader["ProjectDescription"].ToString(),
                        industryId = reader["industryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["industryId"]),
                        amount = reader["Amount"] == DBNull.Value ? string.Empty : reader["Amount"].ToString(),
                        purpose = reader["purpose"] == DBNull.Value ? string.Empty : reader["purpose"].ToString(),
                        loanTypeAutoId = reader["loanTypeAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["loanTypeAutoId"]),
                        statusAutoId = reader["statusAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["statusAutoId"]),
                        industrySectorAutoId = reader["industrySectorAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["industrySectorAutoId"]),
                        assignedToUserId = reader["assignedToUserId"] == DBNull.Value ? string.Empty : reader["assignedToUserId"].ToString(),
                        industrySector = reader["industrySector"] == DBNull.Value ? string.Empty : reader["industrySector"].ToString(),
                        industryDescription = reader["industryDescription"] == DBNull.Value ? string.Empty : reader["industryDescription"].ToString(),
                        assignedToName = reader["assignedToName"] == DBNull.Value ? string.Empty : reader["assignedToName"].ToString(),
                        statusName = reader["statusName"] == DBNull.Value ? string.Empty : reader["statusName"].ToString(),
                        statusDescription = reader["statusDescription"] == DBNull.Value ? string.Empty : reader["statusDescription"].ToString(),
                        loanType = reader["loanType"] == DBNull.Value ? string.Empty : reader["loanType"].ToString(),
                        loanTypeDescription = reader["loanTypeDescription"] == DBNull.Value ? string.Empty : reader["loanTypeDescription"].ToString(),
                    };
                    bu.Add(user);
                }
            }
            return bu;
        }

        public async Task<List<Notes?>> GetNotesByProjectId(int? projectId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.Parameters.AddWithValue("@in_projectId", projectId);
            command.CommandText = @"usp_projectbyNotes";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllNotessAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        private async Task<IReadOnlyList<Notes>> ReadAllNotessAsync(MySqlDataReader reader)
        {
            var bu = new List<Notes>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var Note = new Notes
                    {
                        projectNotesId = reader["projectNotesId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectNotesId"]),
                        tenantId = reader["TenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TenantId"]),
                        projectId = reader["projectId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectId"]),
                        notes = reader["notes"] == DBNull.Value ? string.Empty : reader["notes"].ToString(),
                        createdByUserId = reader["createdByUserId"] == DBNull.Value ? string.Empty : reader["createdByUserId"].ToString(),
                        CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : reader["CreatedByName"].ToString(),
                        createdOn = reader["createdOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["createdOn"].ToString())
                    };
                    bu.Add(Note);
                }
            }
            return bu;
        }

        public async Task<Apistatus> SaveProjectRequest(ProjectRequestData request, string Userid)
        {
            Apistatus apistatus = new Apistatus();
            try
            {
                using (MySqlConnection connection = database.OpenConnection())
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_ProjectRequestWithDetailsInsert";

                        // Convert requestSendTo and requestDetails to JSON
                        string jsonSendTo = JsonConvert.SerializeObject(request.RequestSendTo);
                        string jsonDetails = JsonConvert.SerializeObject(request.RequestDetails);

                        // Add parameters
                        command.Parameters.AddWithValue("@in_requestName", request.RequestName);
                        command.Parameters.AddWithValue("@in_requestDescription", request.RequestDescription);
                        command.Parameters.AddWithValue("@in_projectId", request.ProjectId);
                        command.Parameters.AddWithValue("@in_tenantId", request.TenentId);
                        command.Parameters.AddWithValue("@in_reminderId", request.ReminderId);
                        command.Parameters.AddWithValue("@in_requestSendTo", jsonSendTo);
                        command.Parameters.AddWithValue("@in_requestDetails", jsonDetails);
                        command.Parameters.AddWithValue("@in_createdBy", Userid);

                        // Add output parameter to get the response from the stored procedure


                        MySqlParameter outputParameter = new MySqlParameter("@out_message", MySqlDbType.VarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParameter);

                        command.ExecuteNonQuery();
                        // Get the response message from the stored procedure
                        string message = command.Parameters["@out_message"].Value.ToString();
                        apistatus.message = message;
                        apistatus.code = message == "Insertion successful." ? "200" : "500";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                apistatus.code = "500";
                apistatus.message = ex.Message.ToString();
            }
            return apistatus;
        }

        public async Task<Apistatus> UpdateProjectRequest(ProjectRequestDetailUpdateData data, string? userId)
        {
            Apistatus apistatus = new Apistatus();
            try
            {
                using (MySqlConnection connection = database.OpenConnection())
                {
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_ProjectRequestDataUpdate";

                        command.Parameters.AddWithValue("@in_projectRequestId", data.ProjectRequestId);
                        command.Parameters.AddWithValue("@in_projectId", data.ProjectId);
                        command.Parameters.AddWithValue("@in_tenantId", data.TenantId);
                        command.Parameters.AddWithValue("@in_remindersAutoId", data.RemindersAutoId);
                        command.Parameters.AddWithValue("@in_projectRequestName", data.ProjectRequestName);
                        command.Parameters.AddWithValue("@in_projectRequestDescription", data.ProjectRequestDescription);
                        command.Parameters.AddWithValue("@in_modifiedBy", userId);
                        command.Parameters.AddWithValue("@in_projectRequestSentId", data.ProjectRequestSentId);
                        command.Parameters.AddWithValue("@in_sentTo", data.SentTo);
                        command.Parameters.AddWithValue("@in_sentcc", data.SentCC);
                        command.Parameters.AddWithValue("@in_statusAutoId", data.StatusAutoId);
                        command.Parameters.AddWithValue("@in_projectRequestDetailsId", data.ProjectRequestDetailsId);
                        command.Parameters.AddWithValue("@in_label", data.Label);
                        command.Parameters.AddWithValue("@in_documentTypeAutoId", data.DocumentTypeAutoId);
                        // Add output parameter to get the response from the stored procedure
                        MySqlParameter outputParameter = new MySqlParameter("@out_message", MySqlDbType.VarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParameter);

                        command.ExecuteNonQuery();
                        // Get the response message from the stored procedure
                        string message = command.Parameters["@out_message"].Value.ToString();
                        apistatus.message = message;
                        apistatus.code = message == "Update successful." ? "200" : "500";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                apistatus.code = "500";
                apistatus.message = ex.Message.ToString();
            }
            return apistatus;
        }
    }
}
