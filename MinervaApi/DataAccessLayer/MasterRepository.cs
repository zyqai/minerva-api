using Minerva.Models;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
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
    }
}
