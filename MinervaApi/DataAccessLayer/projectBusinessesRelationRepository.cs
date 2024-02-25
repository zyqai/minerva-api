using Minerva.Models;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models.Requests;
using MySqlConnector;
using System.Data;

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
    }
}
