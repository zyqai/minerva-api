using MinervaApi.IDataAccessLayer;
using MinervaApi.Models.Returns;
using MySqlConnector;
using System.Data;

namespace MinervaApi.DataAccessLayer
{
    public class ProjectRequestRepository : IProjectRequestRepository
    {
        MySqlDataSource database;
        public ProjectRequestRepository(MySqlDataSource database)
        {
            this.database = database;
        }
        public async Task<ProjectRequestDetailsResponse> GetALLAsyncWithProjectId(int projectId)
        {
            List<ProjectRequestResponse> projectRequests = new List<ProjectRequestResponse>();

            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@in_projectId", projectId);
                    command.CommandText = "Usp_projectrequestsList";
                                      
                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            ProjectRequestResponse request = new ProjectRequestResponse();
                            request.projectRequestId = reader.IsDBNull("projectRequestId") ? null : (int?)reader.GetInt32("projectRequestId");
                            request.projectId = reader.IsDBNull("projectId") ? null : (int?)reader.GetInt32("projectId");
                            request.tenantId = reader.IsDBNull("tenantId") ? null : (int?)reader.GetInt32("tenantId");
                            request.projectRequestName = reader.IsDBNull("projectRequestName") ? null : reader.GetString("projectRequestName");
                            request.label = reader.IsDBNull("label") ? null : reader.GetString("label");
                            request.sentTo = reader.IsDBNull("sentTo") ? null : reader.GetString("sentTo");
                            request.sentcc = reader.IsDBNull("sentcc") ? null : reader.GetString("sentcc");
                            request.documentTypeName = reader.IsDBNull("documentTypeName") ? null : reader.GetString("documentTypeName");
                            request.statusName = reader.IsDBNull("statusName") ? null : reader.GetString("statusName");

                            projectRequests.Add(request);
                        }
                    }
                }
            }
            ProjectRequestDetailsResponse response = new ProjectRequestDetailsResponse();
            if (projectRequests != null)
            {
                response.code = "200";
                response.message = "response available";
                response.ProjectRequest= projectRequests;
            }
            else
            {
                response.code = "204";
                response.message = "no content";
            }
            return response;
        }
    }
}
