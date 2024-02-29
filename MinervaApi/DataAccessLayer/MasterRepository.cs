using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
using MinervaApi.Models.Requests;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace MinervaApi.DataAccessLayer
{
    public class MasterRepository : IMasterRepository
    {
        MySqlDataSource database;
        public MasterRepository(MySqlDataSource _database)
        {
            database = _database;
        }
        public async Task<List<Industrys>> GetindustrysAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_industrys";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllindustrysAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }
        private async Task<List<Industrys>> ReadAllindustrysAsync(MySqlDataReader reader)
        {
            var res = new List<Industrys>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var industrys = new Industrys 
                    {
                        industryId= reader["industryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["industryId"]),
                        tenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                        industrySectorAutoId = reader["industrySectorAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["industrySectorAutoId"]),
                        industrySector = reader["industrySector"] == DBNull.Value ? string.Empty : Convert.ToString(reader["industrySector"]),
                        industryDescription = reader["industryDescription"] == DBNull.Value ? string.Empty : Convert.ToString(reader["industryDescription"]),
                    };
                    res.Add(industrys);
                }
            }
            return res;
        }

        public async Task<List<loanTypes>> GetloanTypesAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_loanTypes";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllLoanTypesAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }
        private async Task<List<loanTypes>> ReadAllLoanTypesAsync(MySqlDataReader reader)
        {
            var res = new List<loanTypes>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var re = new loanTypes
                    {
                        loanTypeAutoId = reader["loanTypeAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["loanTypeAutoId"]),
                        tenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                        loanTypeId = reader["loanTypeId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["loanTypeId"]),
                        loanType = reader["loanType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["loanType"]),
                        loanTypeDescription = reader["loanTypeDescription"] == DBNull.Value ? string.Empty : Convert.ToString(reader["loanTypeDescription"]),
                    };
                    res.Add(re);
                }
            }
            return res;
        }

        public async Task<List<Statuses>> GetStatusAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_statuses";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllStatusAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        private async Task<List<Statuses>> ReadAllStatusAsync(MySqlDataReader reader)
        {
            var status=new List<Statuses>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var re = new Statuses
                    {
                        statusAutoId = reader["statusAutoId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["statusAutoId"]),
                        tenantId = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                        statusId = reader["statusId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["statusId"]),
                        statusName = reader["statusName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["statusName"]),
                        statusDescription = reader["statusDescription"] == DBNull.Value ? string.Empty : Convert.ToString(reader["statusDescription"]),
                        projectRequestTemplateStatus = reader["projectRequestTemplateStatus"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["projectRequestTemplateStatus"]),

                    };
                    status.Add(re);
                }
            }
            return status;

        }


        public async Task<Industrys> GetIndustrysByIdAsync(int? id)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_industrys";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllindustrysAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.First();
        }

        public async Task<loanTypes> GetloanTypesByIdAsync(int? id)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_loanTypes";
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllLoanTypesAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.First();
        }

        public async Task<Statuses> GetStatusByIdAsync(int? id)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_statusesById";
            command.Parameters.AddWithValue("@in_statusId", id);
            command.CommandType = CommandType.StoredProcedure;
            var result = await ReadAllStatusAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.First();
        }

        public async Task<Apistatus> SaveNotes(Notes request)
        {
            Apistatus apistatus = new Apistatus();
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"Usp_InsertProjectNotes";
            AddnotesParameters(command, request);
            MySqlParameter outputParameter = new MySqlParameter("@p_projectNotesId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputParameter);
            command.CommandType = CommandType.StoredProcedure;
            int i = await command.ExecuteNonQueryAsync();
            int lastInsertId = Convert.ToInt32(outputParameter.Value);
            connection.Close();
            if (i > 0)
            {

                i = lastInsertId;
                apistatus.code = "200";
                apistatus.message = "notes created successfully";
            }
            else
            {
                apistatus.code = "300";
                apistatus.message = "notes not created try once again";
            }
           return apistatus;
        }

        private void AddnotesParameters(MySqlCommand command, Notes projectNote)
        {
            command.Parameters.AddWithValue("@in_projectId", projectNote.projectId);
            command.Parameters.AddWithValue("@in_tenantId", projectNote.tenantId);
            command.Parameters.AddWithValue("@in_notes", projectNote.notes);
            command.Parameters.AddWithValue("@in_createdByUserId", projectNote.createdByUserId);
        }
    }
}
