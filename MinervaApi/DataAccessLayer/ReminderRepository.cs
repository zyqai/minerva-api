using Minerva.IDataAccessLayer;
using Minerva.Models;
using MySqlConnector;
using System.Data;

namespace Minerva.DataAccessLayer
{
    public class ReminderRepository:IReminderRepository
    {
        MySqlDataSource database;
        public ReminderRepository(MySqlDataSource _dataSource)
        {
            database = _dataSource;
        }

        private void AddParameters(MySqlCommand command, Reminder r)
        {
            command.Parameters.AddWithValue("@p_RemindersId", r.RemindersId);
            command.Parameters.AddWithValue("@p_tenantId", r.TenantId);
            command.Parameters.AddWithValue("@p_Details", r.Details);
        }

        public async Task<bool> SaveReminder(Reminder r)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_ReminderCreate";
                AddParameters(command, r);
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
            catch (Exception)
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


        public async Task<Reminder?> GetReminderAsync(int? remindersAutoId)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_ReminderGetById";
            command.Parameters.AddWithValue("@p_remindersAutoId", remindersAutoId);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return result.FirstOrDefault();
        }

        private async Task<IReadOnlyList<Reminder>> ReadAllAsync(MySqlDataReader reader)
        {
            var Reminders = new List<Reminder>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var ft = new Reminder
                    {
                        remindersAutoId = Convert.ToInt32(reader["remindersAutoId"]),
                        RemindersId = Convert.ToInt32(reader["RemindersId"]),
                        TenantId = Convert.ToInt32(reader["tenantId"]),
                        Details = reader["Details"].ToString()
                    };
                    Reminders.Add(ft);
                }

            }
            return Reminders;
        }
        public async Task<List<Reminder?>> GetALLRemindersAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"usp_RemindersGetAll";
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            connection.Close();
            return [.. result];
        }

        public async Task<bool> UpdateReminder(Reminder r)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = "usp_ReminderUpdate";
                command.Parameters.AddWithValue("@p_remindersAutoId", r.remindersAutoId);
                AddParameters(command, r);
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
            catch (Exception)
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

        public async Task<bool> DeleteReminder(int? remindersAutoId)
        {
            using var connection = database.OpenConnection();
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = "usp_ReminderDelete";
                command.Parameters.AddWithValue("@remindersAutoId", remindersAutoId);
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
            catch (Exception)
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
