using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models.Requests;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace MinervaApi.DataAccessLayer
{
    public class projectBusinessesRelationRepository : IprojectBusinessesRelationRepository
    {
        MySqlDataSource con;
        public projectBusinessesRelationRepository(MySqlDataSource mySql) 
        {
            con = mySql;
        }
        public async Task<int> CreateProjectBusinessRelation(projectBusinessesRelationRequest request)
        {
            try
            {
                using var connection = con.OpenConnection();
                using var command = connection.CreateCommand();
                command.CommandText = @"usp_ProjectBusinessInsert";
                AddUserParameters(command, request);
                command.CommandType = CommandType.StoredProcedure;
                int i = await command.ExecuteNonQueryAsync();
                connection.Close();
                return i;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        private void AddUserParameters(MySqlCommand command, projectBusinessesRelationRequest request)
        {
            command.Parameters.AddWithValue("@in_tenantId", request?.tenantId);
            command.Parameters.AddWithValue("@in_projectId", request?.projectId);
            command.Parameters.AddWithValue("@in_businessId", request?.businessId);
            command.Parameters.AddWithValue("@in_primaryForLoan", request?.primaryForLoan);
            //command.Parameters.AddWithValue("@in_modifiedOn", request?.modifiedOn);
            command.Parameters.AddWithValue("@in_modifiedBy", request?.modifiedBy);

        }

        public async Task<List<BusinessesByProject>> GetProjectByBusinessRelation(int? projectId)
        {
            using var connection = await con.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_ProjectByBusiness";
            command.Parameters.AddWithValue("@in_projectid", projectId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllProjectByBusinessRelationAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result;
        }
        private async Task<List<BusinessesByProject>> ReadAllProjectByBusinessRelationAsync(MySqlDataReader reader)
        {
            List<BusinessesByProject> res = new List<BusinessesByProject>();
            while (await reader.ReadAsync())
            {
                BusinessesByProject peoplesbyproject = new BusinessesByProject
                {
                    projectBusinessId = reader["projectBusinessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectBusinessId"]),
                    businessId = reader["businessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["businessId"]),
                    businessName = reader["businessName"] == DBNull.Value ? string.Empty : reader["businessName"].ToString(),
                    industry = reader["industry"] == DBNull.Value ? string.Empty : reader["industry"].ToString(),
                    annualRevenue = reader["annualRevenue"] == DBNull.Value ? string.Empty : reader["annualRevenue"].ToString(),
                    businessAddress = reader["businessAddress"] == DBNull.Value ? string.Empty : reader["businessAddress"].ToString(),
                    peopleid = reader["peopleid"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["peopleid"]),
                    clientName = reader["clientname"] == DBNull.Value ? string.Empty : reader["clientname"].ToString(),
                    projectName = reader["projectName"] == DBNull.Value ? string.Empty : reader["projectName"].ToString(),
                    projectId = reader["projectId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectid"]),
                };
                res.Add(peoplesbyproject);
            }
            return res;
        }

        public async Task<List<ResponceprojectBusinessesRelation?>?> GetBusinessByProjectid(int? projectId)
        {
            using var connection = await con.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_projectBusinessRelationwithProjectId";
            command.Parameters.AddWithValue("@in_projectid", projectId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllProjectByBusinessListAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result;
        }

        private async Task<List<ResponceprojectBusinessesRelation?>?> ReadAllProjectByBusinessListAsync(MySqlDataReader reader)
        {
            List<ResponceprojectBusinessesRelation> res = new List<ResponceprojectBusinessesRelation>();
            while (await reader.ReadAsync())
            {
                ResponceprojectBusinessesRelation peoplesbyproject = new ResponceprojectBusinessesRelation
                {
                    projectBusinessId = reader["projectBusinessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectBusinessId"]),
                    businessId = reader["businessId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["businessId"]),
                    businessName = reader["businessName"] == DBNull.Value ? string.Empty : reader["businessName"].ToString(),
                    businessType = reader["businessType"] == DBNull.Value ? string.Empty : reader["businessType"].ToString(),
                    projectId = reader["projectId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectid"]),
                };
                res.Add(peoplesbyproject);
            }
            return res;
        }
    }
}
