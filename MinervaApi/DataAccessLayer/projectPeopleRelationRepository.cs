using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

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
        public async Task<List<Peoplesbyproject>> GetPeopleByProjectId(int? projectId)
        {
            using var connection = await con.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_projectBypeople";
            command.Parameters.AddWithValue("@in_projectid", projectId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllPeopleByProjectsAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result;
        }

        private async Task<List<Peoplesbyproject>> ReadAllPeopleByProjectsAsync(MySqlDataReader reader)
        {
            List<Peoplesbyproject> res = new List<Peoplesbyproject>();
            while (await reader.ReadAsync()) 
            {
                Peoplesbyproject peoplesbyproject = new Peoplesbyproject 
                {
                    clientname = reader["clientname"] == DBNull.Value ? string.Empty : reader["clientname"].ToString(),
                    firstname = reader["firstname"] == DBNull.Value ? string.Empty : reader["firstname"].ToString(),
                    lastname = reader["lastname"] == DBNull.Value ? string.Empty : reader["lastname"].ToString(),
                    phonenumber = reader["phonenumber"] == DBNull.Value ? string.Empty : reader["phonenumber"].ToString(),
                    email = reader["email"] == DBNull.Value ? string.Empty : reader["email"].ToString(),
                    projectName = reader["projectName"] == DBNull.Value ? string.Empty : reader["projectName"].ToString(),
                    amount = reader["amount"] == DBNull.Value ? string.Empty : reader["amount"].ToString(),
                    personaName = reader["personaName"] == DBNull.Value ? string.Empty : reader["personaName"].ToString(),
                    projectPersona = reader["projectPersona"] == DBNull.Value ? string.Empty : reader["projectPersona"].ToString(),
                    purpose = reader["purpose"] == DBNull.Value ? string.Empty : reader["purpose"].ToString(),
                    personaAutoId = reader["personaAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaAutoId"]),
                    personaId = reader["personaId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaId"]),
                    projectid = reader["projectid"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectid"]),
                };
                res.Add(peoplesbyproject);
            }
            return res;
        }

        public async Task<List<ResponceprojectPeopleRelation?>?> GetPeopleDetailsByProjectId(int? projectId)
        {
            using var connection = await con.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_ProjectPeopleRelationsWithProjectId";
            command.Parameters.AddWithValue("@in_projectid", projectId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllPeopleByProjectsRelationAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result;
        }

        private async Task<List<ResponceprojectPeopleRelation?>?> ReadAllPeopleByProjectsRelationAsync(MySqlDataReader reader)
        {
            List<ResponceprojectPeopleRelation> res = new List<ResponceprojectPeopleRelation>();
            while (await reader.ReadAsync())
            {
                ResponceprojectPeopleRelation peoplesbyproject = new ResponceprojectPeopleRelation
                {
                    clientName = reader["clientname"] == DBNull.Value ? string.Empty : reader["clientname"].ToString(),
                    firstName = reader["firstname"] == DBNull.Value ? string.Empty : reader["firstname"].ToString(),
                    lastName = reader["lastname"] == DBNull.Value ? string.Empty : reader["lastname"].ToString(),
                    phoneNumber = reader["phonenumber"] == DBNull.Value ? string.Empty : reader["phonenumber"].ToString(),
                    email = reader["email"] == DBNull.Value ? string.Empty : reader["email"].ToString(),
                    personaName = reader["personaName"] == DBNull.Value ? string.Empty : reader["personaName"].ToString(),
                    projectPersona = reader["projectPersona"] == DBNull.Value ? string.Empty : reader["projectPersona"].ToString(),
                    personaAutoId = reader["personaAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaAutoId"]),
                    personaId = reader["personaId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["personaId"]),
                    ProjectId = reader["ProjectId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ProjectId"]),
                    peopleId = reader["peopleId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["peopleId"]),
                    tenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                    userId = reader["userId"] == DBNull.Value ? string.Empty : reader["userId"].ToString(),
                    primaryBorrower = reader["primaryBorrower"] == DBNull.Value ? string.Empty : reader["primaryBorrower"].ToString(),
                    projectPeopleId= reader["projectPeopleId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectPeopleId"]),
                };
                res.Add(peoplesbyproject);
            }
            return res;
        }

        public async Task<bool> DeleteProjectPeopleRelation(int projectPeopleId)
        {
            using var connection = con.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ProjectPeopleDelete";
            command.Parameters.AddWithValue("@in_projectPeopleId", projectPeopleId);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            connection.Close();
            return i >= 1 ? true : false;
        }
    }
}
