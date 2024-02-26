using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.Models.Requests;

namespace Minerva.BusinessLayer
{
    public class ReminderBL:IReminderBL
    {
        IReminderRepository Reminderrepository;
        public ReminderBL(IReminderRepository _repository)
        {
            Reminderrepository = _repository;
        }


        public Task<bool> DeleteReminder(int remindersAutoId)
        {
            return Reminderrepository.DeleteReminder(remindersAutoId);
        }

        public Task<List<Reminder?>> GetALLReminders()
        {
            return Reminderrepository.GetALLRemindersAsync();
        }

        public Task<Reminder?> GetReminder(int remindersAutoId)
        {
            return Reminderrepository.GetReminderAsync(remindersAutoId);
        }

        public Task<bool> SaveReminder(ReminderRequest request)
        {
            Reminder Reminder = Mapping(request);
            return Reminderrepository.SaveReminder(Reminder);
        }

        public Task<bool> UpdateReminder(ReminderRequest request)
        {
            Reminder Reminder = Mapping(request);
            return Reminderrepository.UpdateReminder(Reminder);
        }


        private Reminder Mapping(ReminderRequest request)
        {
            Reminder r = new Reminder();

            r.remindersAutoId = request.remindersAutoId;

            r.RemindersId = request.RemindersId;

            r.TenantId = request.TenantId;

            r.Details = request.Details;

            return r;
        }

    }
}
