using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.BusinessLayer;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
using MySqlConnector;
using System.Data;
using System.Xml.Linq;

namespace MinervaApi.DataAccessLayer
{
    public class LenderRepository : ILenderRepository
    {
        MySqlDataSource db;
        public LenderRepository(MySqlDataSource database)
        {
            db = database;
        }

        public async Task<Lender?> GetLender(int lenderId)
        {
            using var connection = await db.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"Usp_LenderSelectById";
            command.Parameters.AddWithValue("@in_lenderID", lenderId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<List<Lender>> ReadAllAsync(MySqlDataReader reader)
        {
            List<Lender> lenderList = new List<Lender>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var len = new Lender
                    {
                        LenderID = reader["lenderID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["lenderID"]),
                        TenantID = reader["tenantId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["tenantId"]),
                        Name = reader["name"] == DBNull.Value ? string.Empty : reader["name"].ToString(),
                        ContactAddress = reader["contactAddress"] == DBNull.Value ? string.Empty : reader["contactAddress"].ToString(),
                        PhoneNumber = reader["phoneNumber"] == DBNull.Value ? string.Empty : reader["phoneNumber"].ToString(),
                        Email = reader["email"] == DBNull.Value ? string.Empty : reader["email"].ToString(),
                        LicensingDetails = reader["licensingDetails"] == DBNull.Value ? string.Empty : reader["licensingDetails"].ToString(),
                        CommercialMortgageProducts = reader["commercialMortgageProducts"] == DBNull.Value ? string.Empty : reader["commercialMortgageProducts"].ToString(),
                        InterestRates = reader["interestRates"] == DBNull.Value ?0 : Convert.ToDecimal(reader["interestRates"]),
                        Terms = reader["terms"] == DBNull.Value ? string.Empty : reader["terms"].ToString(),
                        LoanToValueRatio = reader["loanToValueRatio"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["loanToValueRatio"]),
                        ApplicationProcessDetails = reader["applicationProcessDetails"] == DBNull.Value ? string.Empty : reader["applicationProcessDetails"].ToString(),
                        UnderwritingGuidelines = reader["underwritingGuidelines"] == DBNull.Value ? string.Empty : reader["underwritingGuidelines"].ToString(),
                        ClosingCostsAndFees = reader["closingCostsAndFees"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["closingCostsAndFees"]),
                        SpecializedServices = reader["specializedServices"] == DBNull.Value ? string.Empty : reader["specializedServices"].ToString(),
                    };
                    lenderList.Add(len);
                }
            }
            return lenderList;
        }

        public async Task<int> SaveLender(Lender req)
        {
            using var connection = db.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = @"Usp_LenderInsert";
            AddParameters(command, req);
            MySqlParameter outputParameter = new MySqlParameter("@p_last_insert_id", SqlDbType.Int)
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
            }
            return i;
        }

        private void AddParameters(MySqlCommand command, Lender req)
        {
            command.Parameters.AddWithValue("@in_tenantId", req.TenantID);
            command.Parameters.AddWithValue("@in_name", req.Name);
            command.Parameters.AddWithValue("@in_contactAddress", req.ContactAddress);
            command.Parameters.AddWithValue("@in_phoneNumber", req.PhoneNumber);
            command.Parameters.AddWithValue("@in_email", req.Email);
            command.Parameters.AddWithValue("@in_licensingDetails", req.LicensingDetails);
            command.Parameters.AddWithValue("@in_commercialMortgageProducts", req.CommercialMortgageProducts);
            command.Parameters.AddWithValue("@in_interestRates", req.InterestRates);
            command.Parameters.AddWithValue("@in_terms", req.Terms);
            command.Parameters.AddWithValue("@in_loanToValueRatio", req.LoanToValueRatio);
            command.Parameters.AddWithValue("@in_applicationProcessDetails", req.ApplicationProcessDetails);
            command.Parameters.AddWithValue("@in_underwritingGuidelines", req.UnderwritingGuidelines);
            command.Parameters.AddWithValue("@in_closingCostsAndFees", req.ClosingCostsAndFees);
            command.Parameters.AddWithValue("@in_specializedServices", req.SpecializedServices);
        }

        public async Task<List<Lender?>> GetALLLenders()
        {
            using var connection = await db.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"Usp_LenderSelectAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<Apistatus> UpdateLendet(Lender lender)
        {
            string message = string.Empty;
            try
            {
                using (var connection = db.OpenConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        
                        command.CommandText = "Usp_UpdateLender";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@in_lenderID", lender.LenderID);
                        command.Parameters.AddWithValue("@in_name", lender.Name);
                        command.Parameters.AddWithValue("@in_contactAddress", lender.ContactAddress);
                        command.Parameters.AddWithValue("@in_phoneNumber", lender.PhoneNumber);
                        command.Parameters.AddWithValue("@in_email", lender.Email);
                        command.Parameters.AddWithValue("@in_licensingDetails", lender.LicensingDetails);
                        command.Parameters.AddWithValue("@in_commercialMortgageProducts", lender.CommercialMortgageProducts);
                        command.Parameters.AddWithValue("@in_interestRates", lender.InterestRates);
                        command.Parameters.AddWithValue("@in_terms", lender.Terms);
                        command.Parameters.AddWithValue("@in_loanToValueRatio", lender.LoanToValueRatio);
                        command.Parameters.AddWithValue("@in_applicationProcessDetails", lender.ApplicationProcessDetails);
                        command.Parameters.AddWithValue("@in_underwritingGuidelines", lender.UnderwritingGuidelines);
                        command.Parameters.AddWithValue("@in_closingCostsAndFees", lender.ClosingCostsAndFees);
                        command.Parameters.AddWithValue("@in_specializedServices", lender.SpecializedServices);
                        
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            message = result.ToString();
                        }
                        connection.Clone();
                    }
                }
            }
            catch (MySqlException ex)
            {
                message = "Error occurred: " + ex.Message;
            }
            finally 
            { 
                
            }
            Apistatus apistatus = new Apistatus();
            if (message == "Update successful.")
            {
                apistatus.code = "200";
                apistatus.message = message;
            }
            else
            {
                apistatus.code = "500";
                apistatus.message = message;
            }
            return apistatus;
        }

        public async Task<Apistatus> DeleteLender(int lenderId)
        {
            Apistatus apistatus = new Apistatus();
            string message = string.Empty;
            try
            {
                using (var connection = db.OpenConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@in_lenderID", lenderId);
                        command.CommandText = "Usp_LenderDelete";
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            message = result.ToString();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                message = "Error occurred: " + ex.Message;
            }
            
            apistatus.message = message;
            if (message == "Deletion successful.")
            {
                apistatus.code = "200";
            }
            else
            {
                apistatus.code = "500";
            }
            return apistatus;
        }
    }
}
