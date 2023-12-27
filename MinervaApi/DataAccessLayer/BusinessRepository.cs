using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;

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
