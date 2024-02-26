using Minerva.Models.Requests;
using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    public interface IReminderBL
    {

        public Task<bool> SaveReminder(ReminderRequest request);
        public Task<Reminder?> GetReminder(int RemindersAutoId);
        public Task<List<Reminder?>> GetALLReminders();
        public Task<bool> UpdateReminder(ReminderRequest request);
        public Task<bool> DeleteReminder(int RemindersAutoId);

    }
}
