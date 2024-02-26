using Minerva.Models;

namespace Minerva.IDataAccessLayer
{
    public interface IReminderRepository
    {
        public Task<Reminder?> GetReminderAsync(int? remindersAutoId);
        public Task<List<Reminder?>> GetALLRemindersAsync();
        public Task<bool> SaveReminder(Reminder us);
        public Task<bool> UpdateReminder(Reminder us);
        public Task<bool> DeleteReminder(int? remindersAutoId);
    }
}
