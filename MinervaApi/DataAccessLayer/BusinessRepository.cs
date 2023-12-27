using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Minerva.DataAccessLayer
{
    public class BusinessRepository : IBusinessRepository
    {
        MySqlDataSource database;
        public BusinessRepository(MySqlDataSource database)
        {
            this.database = database;
        }
        public bool SaveBusiness(Business bs)
        {
            using var connection = database.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_CreateBusiness";
            AddUserParameters(command, bs);
            command.CommandType = CommandType.StoredProcedure;
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Business?> GetuserAsync(int BusinessId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"USP_ReadBusinessID";
            command.Parameters.AddWithValue("@in_businessId", BusinessId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<IReadOnlyList<Business>> ReadAllAsync(MySqlDataReader reader)
        {
            var bu = new List<Business>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new Business
                    {
                        BusinessId = reader.GetInt32(0),
                        TenantId=reader.GetInt32(1),
                        BusinessName= reader.GetValue(2).ToString(),
                        BusinessAddress= reader.GetValue(3).ToString(),
                        Industry  =reader.GetValue(4).ToString(),
                        AnnualRevenue=reader.GetDecimal(5),
                        IncorporationDate=reader.GetDateTime(6),
                        BusinessRegistrationNumber =reader.GetValue(7).ToString(),
                        RootDocumentFolder=reader.GetValue(8).ToString(),
                    };
                }
            }
            return bu;
        }

        private void AddUserParameters(MySqlCommand command, Business bs)
        {
            command.Parameters.AddWithValue("@in_businessId", bs.BusinessId);
            command.Parameters.AddWithValue("@in_tenantId", bs.TenantId);
            command.Parameters.AddWithValue("@in_businessName", bs.BusinessName);
            command.Parameters.AddWithValue("@in_businessAddress", bs.BusinessAddress);
            command.Parameters.AddWithValue("@in_businessType", bs.BusinessType);
            command.Parameters.AddWithValue("@in_industry", bs.Industry);
            command.Parameters.AddWithValue("@in_annualRevenue", bs.AnnualRevenue);
            command.Parameters.AddWithValue("@in_incorporationDate", bs.IncorporationDate);
            command.Parameters.AddWithValue("@in_businessRegistrationNumber", bs.BusinessRegistrationNumber);
            command.Parameters.AddWithValue("@in_rootDocumentFolder", bs.RootDocumentFolder);
        }
    }
}
