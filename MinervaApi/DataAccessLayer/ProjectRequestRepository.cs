using IdentityModel.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using Minerva.BusinessLayer;
using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.ExternalApi;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
using MinervaApi.Models.Requests;
using MinervaApi.Models.Returns;
using MySqlConnector;
using System.Collections;
using System.Data;
using System.Reflection.Emit;
using System.Security.Cryptography;

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
                            request.documentClassificationName = reader.IsDBNull("documentClassificationName") ? null : reader.GetString("documentClassificationName");
                            request.createdOn = reader.IsDBNull("createdOn") ? null : reader.GetDateTime("createdOn");
                            request.modifiedOn = reader.IsDBNull("modifiedOn") ? null : reader.GetDateTime("modifiedOn");
                            request.Assignedto = "Rajendra Prasad";// we want to change this from Database it's temp
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
                response.ProjectRequest = projectRequests;
            }
            else
            {
                response.code = "204";
                response.message = "no content";
            }
            return response;
        }

        public async Task<ProjectRequest?> GetAllProjectRequestById(int projectRequestId)
        {
            ProjectRequest request = new ProjectRequest();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@in_projectRequestId", projectRequestId);
                    command.CommandText = "Usp_projectRequestById";

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            request.ProjectRequestId = reader.IsDBNull("projectRequestId") ? null : (int?)reader.GetInt32("projectRequestId");
                            request.ProjectId = reader.IsDBNull("projectId") ? null : (int?)reader.GetInt32("projectId");
                            request.TenantId = reader.IsDBNull("tenantId") ? null : (int?)reader.GetInt32("tenantId");
                            request.RemindersAutoId = reader.IsDBNull("remindersAutoId") ? null : (int?)reader.GetInt32("remindersAutoId");
                            request.ProjectRequestName = reader.IsDBNull("projectRequestName") ? null : reader.GetString("projectRequestName");
                            request.ProjectRequestDescription = reader.IsDBNull("projectRequestDescription") ? null : reader.GetString("projectRequestDescription");
                            request.CreatedBy = reader.IsDBNull("CreatedBy") ? null : reader.GetString("CreatedBy");
                            request.CreatedOn = reader.IsDBNull("createdOn") ? null : reader.GetDateTime("createdOn");
                            request.ModifiedBy = reader.IsDBNull("ModifiedBy") ? null : reader.GetString("ModifiedBy");
                            request.ModifiedOn = reader.IsDBNull("ModifiedOn") ? null : reader.GetDateTime("ModifiedOn");
                        }
                    }
                }
            }
            return request;
        }

        public async Task<List<Models.Returns.ProjectRequestSentTo>?> GetAllProjectRequestSentToByRequestId(int projectRequestId)
        {
            List<Models.Returns.ProjectRequestSentTo> requestSentTos = new List<Models.Returns.ProjectRequestSentTo>();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@in_ProjectRequestId", projectRequestId);
                    command.CommandText = "Usp_projectrequestsenttoByProjectRequestID";

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Models.Returns.ProjectRequestSentTo request = new Models.Returns.ProjectRequestSentTo
                            {
                                ProjectRequestTemplateId = reader.IsDBNull("projectRequestTemplateId") ? null : (int?)reader.GetInt32("projectRequestTemplateId"),
                                ProjectId = reader.IsDBNull("projectId") ? null : (int?)reader.GetInt32("projectId"),
                                TenantId = reader.IsDBNull("tenantId") ? null : (int?)reader.GetInt32("tenantId"),
                                ProjectRequestSentId = reader.IsDBNull("projectRequestSentId") ? null : (int?)reader.GetInt32("projectRequestSentId"),
                                SentTo = reader.IsDBNull("sentTo") ? null : reader.GetString("sentTo"),
                                SentCC = reader.IsDBNull("sentcc") ? null : reader.GetString("sentcc"),
                                SentOn = reader.IsDBNull("sentOn") ? null : reader.GetDateTime("sentOn"),
                                UniqueLink = reader.IsDBNull("uniqueLink") ? null : reader.GetString("uniqueLink"),
                                StatusAutoId = reader.IsDBNull("statusAutoId") ? null : (int?)reader.GetInt32("statusAutoId"),
                                statusName = reader.IsDBNull("statusName") ? null : reader.GetString("statusName"),
                                statusDescription = reader.IsDBNull("statusDescription") ? null : reader.GetString("statusDescription"),
                            };
                            requestSentTos.Add(request);

                        }
                    }
                }
            }
            return requestSentTos;
        }

        public async Task<List<Models.Returns.ProjectRequestDetail>?> GetAllProjectRequestDetailsByRequestId(int projectRequestId)
        {
            List<Models.Returns.ProjectRequestDetail> projectRequestDetails = new List<Models.Returns.ProjectRequestDetail>();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@in_projectRequestId", projectRequestId);
                    command.CommandText = "Usp_ProjectRequestDetailsByProjectRequestId";

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Models.Returns.ProjectRequestDetail request = new Models.Returns.ProjectRequestDetail
                            {
                                ProjectRequestTemplateId = reader.IsDBNull("projectRequestTemplateId") ? null : (int?)reader.GetInt32("projectRequestTemplateId"),
                                ProjectId = reader.IsDBNull("projectId") ? null : (int?)reader.GetInt32("projectId"),
                                TenantId = reader.IsDBNull("tenantId") ? null : (int?)reader.GetInt32("tenantId"),
                                ProjectRequestDetailsId = reader.IsDBNull("projectrequestDetailsId") ? null : (int?)reader.GetInt32("projectrequestDetailsId"),
                                DocumentTypeAutoId = reader.IsDBNull("documentTypeAutoId") ? null : (int?)reader.GetInt32("documentTypeAutoId"),
                                Label = reader.IsDBNull("label") ? null : reader.GetString("label"),
                                DocumentTypeName = reader.IsDBNull("documentTypeName") ? null : reader.GetString("documentTypeName"),
                                DocumentTypeDescription = reader.IsDBNull("documentTypeDescription") ? null : reader.GetString("documentTypeDescription"),
                                TemplateFilePath = reader.IsDBNull("templateFilePath") ? null : reader.GetString("templateFilePath"),
                            };
                            projectRequestDetails.Add(request);

                        }
                    }
                }
            }
            return projectRequestDetails;
        }

        public async Task<APIStatus> SaveProjectRequestDetails(Models.ProjectRequestDetail prd)
        {
            APIStatus status = new APIStatus();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Usp_ProjectRequestDetailsInsert";
                    command.Parameters.AddWithValue("@in_projectrequestTemplateId", prd.ProjectRequestTemplateId);
                    command.Parameters.AddWithValue("@in_projectId", prd.ProjectId);
                    command.Parameters.AddWithValue("@in_tenantId", prd.TenantId);
                    command.Parameters.AddWithValue("@in_label", prd.Label);
                    command.Parameters.AddWithValue("@in_documentTypeAutoId", prd.DocumentTypeAutoId);
                    // Add output parameter
                    command.Parameters.Add(new MySqlParameter("@out_message", MySqlDbType.VarChar, 1000));
                    command.Parameters["@out_message"].Direction = ParameterDirection.Output;
                    // Execute command
                    await command.ExecuteNonQueryAsync();
                    // Get output message
                    string message = command.Parameters["@out_message"]?.Value?.ToString() ?? string.Empty;
                        status.Message = message;
                    status.Code = message == "Insertion successful." ? "200" : "500";
                }
            }
            return status;
        }

        public async Task<APIStatus> UpdateProjectRequestDetails(Models.ProjectRequestDetail prd)
        {
            APIStatus status = new APIStatus();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Usp_ProjectRequestDetailsUpdate";
                    command.Parameters.AddWithValue("@in_projectrequestDetailsId", prd.ProjectRequestDetailsId);
                    command.Parameters.AddWithValue("@in_projectrequestTemplateId", prd.ProjectRequestTemplateId);
                    command.Parameters.AddWithValue("@in_projectId", prd.ProjectId);
                    command.Parameters.AddWithValue("@in_tenantId", prd.TenantId);
                    command.Parameters.AddWithValue("@in_label", prd.Label);
                    command.Parameters.AddWithValue("@in_documentTypeAutoId", prd.DocumentTypeAutoId);
                    // Add output parameter
                    command.Parameters.Add(new MySqlParameter("@out_message", MySqlDbType.VarChar, 1000));
                    command.Parameters["@out_message"].Direction = ParameterDirection.Output;
                    // Execute command
                    await command.ExecuteNonQueryAsync();
                    // Get output message
                    string message = command.Parameters["@out_message"]?.Value?.ToString() ?? string.Empty;
                    status.Message = message;
                    status.Code = message == "Update successful." ? "200" : "500";
                }
            }
            return status;
        }

        public async Task<APIStatus> SaveProjectRequestSentTo(Models.ProjectRequestSentTo prst)
        {
            APIStatus status = new APIStatus();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Usp_ProjectRequestSentToInsert";
                    command.Parameters.AddWithValue("@in_projectRequestTemplateId", prst.ProjectRequestTemplateId);
                    command.Parameters.AddWithValue("@in_projectId", prst.ProjectId);
                    command.Parameters.AddWithValue("@in_tenantId", prst.TenantId);
                    command.Parameters.AddWithValue("@in_sentTo", prst.SentTo);
                    command.Parameters.AddWithValue("@in_sentcc", prst.SentCC);
                    command.Parameters.AddWithValue("@in_uniqueLink", prst.UniqueLink);
                    command.Parameters.AddWithValue("@in_statusAutoId", prst.StatusAutoId);
                    MySqlParameter outputParameter = new MySqlParameter("@out_message", MySqlDbType.VarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);
                    await command.ExecuteNonQueryAsync();
                    string message = command.Parameters["@out_message"]?.Value?.ToString() ?? string.Empty;
                    status.Message = message;
                    status.Code = message == "Insertion successful." ? "200" : "500";
                }
            }
            return status;
        }

        public async Task<APIStatus> UpdateProjectRequestSentTo(Models.ProjectRequestSentTo prst)
        {
            APIStatus status = new APIStatus();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Usp_ProjectRequestSentToUpdate";
                    command.Parameters.AddWithValue("@in_projectRequestSentId", prst.ProjectRequestSentId);
                    command.Parameters.AddWithValue("@in_projectRequestTemplateId", prst.ProjectRequestTemplateId);
                    command.Parameters.AddWithValue("@in_projectId", prst.ProjectId);
                    command.Parameters.AddWithValue("@in_tenantId", prst.TenantId);
                    command.Parameters.AddWithValue("@in_sentTo", prst.SentTo);
                    command.Parameters.AddWithValue("@in_sentcc", prst.SentCC);
                    command.Parameters.AddWithValue("@in_statusAutoId", prst.StatusAutoId);
                    MySqlParameter outputParameter = new MySqlParameter("@out_message", MySqlDbType.VarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);
                    await command.ExecuteNonQueryAsync();
                    string message = command.Parameters["@out_message"]?.Value?.ToString() ?? string.Empty;
                    status.Message = message;
                    status.Code = message == "Update successful." ? "200" : "500";
                }
            }
            return status;
        }

        public async Task<ProjectRequestUrl> GetAllProjectRequestBytoken(string? token)
        {
            ProjectRequestUrl response = new ProjectRequestUrl();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    string tok=Comman.DecryptString(token);
                    command.Parameters.AddWithValue("@in_token", tok);
                    command.CommandText = "USP_GetProjectURL ";
                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            response.TenantId= reader.IsDBNull("tenantId") ? null : (int?)reader.GetInt32("tenantId");
                            response.ProjectId= reader.IsDBNull("projectId") ? null : (int?)reader.GetInt32("projectId");
                            response.PeopleId= reader.IsDBNull("peopleId") ? null : (int?)reader.GetInt32("peopleId");
                            response.ProjectRequestId = reader.IsDBNull("projectRequestId") ? null : (int?)reader.GetInt32("projectRequestId");
                            response.Token=reader.IsDBNull("token") ?null: (string)reader.GetValue("token");
                            response.RequestURL = reader.IsDBNull("requestURL") ?null: (string)reader.GetValue("requestURL");
                        }
                    }
                }
            }
           return response;
        }

        public async Task<List<ProjectRequestDetails?>> GetAllProjectRequestDetailsByProjectid(int prid)
        {
            List<ProjectRequestDetails> projectRequestDetails = new List<ProjectRequestDetails>();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@in_project", prid);
                    command.CommandText = "USP_projectDocumentDetails";

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            ProjectRequestDetails request = new ProjectRequestDetails
                            {
                                ProjectRequestDetailsId = reader.IsDBNull("projectrequestDetailsId") ? null : (int?)reader.GetInt32("projectrequestDetailsId"),
                                ProjectRequestTemplateId = reader.IsDBNull("projectRequestTemplateId") ? null : (int?)reader.GetInt32("projectRequestTemplateId"),
                                ProjectId = reader.IsDBNull("projectId") ? null : (int?)reader.GetInt32("projectId"),
                                TenantId = reader.IsDBNull("tenantId") ? null : (int?)reader.GetInt32("tenantId"),
                                documentTypeAutoId = reader.IsDBNull("documentTypeAutoId") ? null : (int?)reader.GetInt32("documentTypeAutoId"),
                                documentTypeId = reader.IsDBNull("documentTypeId") ? null : (int?)reader.GetInt32("documentTypeId"),
                                Label = reader.IsDBNull("label") ? null : reader.GetString("label"),
                                DocumentTypeName = reader.IsDBNull("documentTypeName") ? null : reader.GetString("documentTypeName"),
                                DocumentTypeDescription = reader.IsDBNull("documentTypeDescription") ? null : reader.GetString("documentTypeDescription"),
                                DocumentClassificationName = reader.IsDBNull("documentClassificationName") ? null : reader.GetString("documentClassificationName"),
                                documentClassificationAutoId = reader.IsDBNull("documentClassificationAutoId") ? null : (int?)reader.GetInt32("documentClassificationAutoId"),
                                documentClassificationId = reader.IsDBNull("documentClassificationId") ? null : (int?)reader.GetInt32("documentClassificationId"),
                            };
                            projectRequestDetails.Add(request);
                        }
                    }
                }
            }
            return projectRequestDetails;
        }

        public async Task<APIStatus> projectRequestUpdateStatus(UpdateProjectRequestSentId request)
        {
            APIStatus status = new APIStatus();
            using (MySqlConnection connection = database.OpenConnection())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Usp_projectRequestUpdateStatus";
                    command.Parameters.AddWithValue("@in_projectRequestSentId", request.ProjectRequestSentId);
                    command.Parameters.AddWithValue("@in_statusAutoId", request.StatusAutoId);
                    int i=await command.ExecuteNonQueryAsync();
                    if (i > 0)
                    {
                        status.Code = "200";
                        status.Message = "Update request status";
                    }
                    else
                    {
                        status.Code = "204";
                        status.Message = "No content";
                    }
                }
            }
            return status;
        }
    }
}
