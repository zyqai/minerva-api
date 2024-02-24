using Minerva.Models;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
using MySqlConnector;
using System.Data;

namespace MinervaApi.DataAccessLayer
{
    public class projectPeopleRelationRepository : IprojectPeopleRelationRepository
    {
        MySqlDataSource con;
        public projectPeopleRelationRepository(MySqlDataSource mySql)
        {
            con = mySql;
        }

        public async Task<int> CreateprojectPeopleRelation(projectPeopleRelation? relation)
        {
            try
            {
                using var connection = con.OpenConnection();
                using var command = connection.CreateCommand();
                command.CommandText = @"ups_ProjectPeopleInsert";
                AddUserParameters(command, relation);
                //MySqlParameter outputParameter = new MySqlParameter("@p_last_insert_id", SqlDbType.Int)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //command.Parameters.Add(outputParameter);
                command.CommandType = CommandType.StoredProcedure;
                int i = await command.ExecuteNonQueryAsync();
                //int lastInsertId = Convert.ToInt32(outputParameter.Value);
                connection.Close();
                //if (i > 0)
                //{
                //    i = lastInsertId;
                //}
                return i;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void AddUserParameters(MySqlCommand command, projectPeopleRelation? p)
        {
            command.Parameters.AddWithValue("@in_tenantId", p?.tenantId);
            command.Parameters.AddWithValue("@in_projectId", p?.projectId);
            command.Parameters.AddWithValue("@in_peopleId", p?.peopleId);
            command.Parameters.AddWithValue("@in_primaryBorrower", p?.primaryBorrower);
            command.Parameters.AddWithValue("@in_personaAutoId", p?.personaAutoId);
        }

    }
}
