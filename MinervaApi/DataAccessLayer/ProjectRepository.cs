using Minerva.BusinessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace MinervaApi.DataAccessLayer
{
    public class ProjectRepository :IProjectRepository
    {
        MySqlDataSource database;
        public ProjectRepository(MySqlDataSource mySql)
        {
            this.database = mySql;
        }
       
        public async Task<bool> SaveProject(Project p)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "USP_InsertProject";
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
            catch (Exception ex)
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
        
        private void AddParameters(MySqlCommand command, Project p)
        {
            command.Parameters.AddWithValue("@p_Filename", p.Filename);
            command.Parameters.AddWithValue("@p_Loanamount", p.Loanamount);
            command.Parameters.AddWithValue("@p_Assignrdstaff", p.Assignrdstaff);
            command.Parameters.AddWithValue("@p_Filedescription", p.Filedescription);
            command.Parameters.AddWithValue("@p_staffnote", p.Staffnote);
            command.Parameters.AddWithValue("@p_primaryborrower", p.Primaryborrower);
            command.Parameters.AddWithValue("@p_Primarybusiness", p.Primarybusiness);
            command.Parameters.AddWithValue("@p_startdate", p.Startdate);
            command.Parameters.AddWithValue("@p_desiredclosingdate", p.Desiredclosingdate);
            command.Parameters.AddWithValue("@p_initialphase", p.Initialphase);
        }

        public async Task<Project?> GetProjectAsync(int Id_Projects)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_GetProjectById";
            command.Parameters.AddWithValue("@p_id", Id_Projects);
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
                        Id_Projects = reader.GetInt32(0),
                        Filename = reader.GetValue(1).ToString(),
                        Loanamount = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2),
                        Assignrdstaff = reader.GetValue(3).ToString(),
                        Filedescription = reader.GetValue(4).ToString(),
                        Staffnote = reader.GetValue(5).ToString(),
                        Primaryborrower = reader.GetValue(6).ToString(),
                        Primarybusiness = reader.GetValue(7).ToString(),
                        Startdate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                        Desiredclosingdate= reader.IsDBNull(9) ? (DateTime?)null : reader.GetDateTime(9),
                        Initialphase = reader.GetValue(10).ToString(),
                        CreateDateTime= reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
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
            command.CommandText = @"USP_GetProjects";
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

                command.CommandText = "USP_UpdateProject";
                command.Parameters.AddWithValue("@p_id", p.Id_Projects);
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
            catch (Exception ex)
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
                command.Parameters.AddWithValue("@p_id", Id_Projects);
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
            catch (Exception ex)
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
