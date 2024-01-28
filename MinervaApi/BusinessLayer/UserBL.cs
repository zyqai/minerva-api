using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models.Returns;

namespace Minerva.BusinessLayer
{
    public class UserBL : IUserBL
    {
        IUserRepository UserRepository;
        public UserBL(IUserRepository _userRepository) {
            UserRepository = _userRepository;
        }
        public Task<User?>GetUser(UsersRequest u) 
        {
            return UserRepository.GetuserAsync(u.UserId);
        }
        public Task<User?> GetUserusingUserName(UsersRequest u)
        {
            return UserRepository.GetuserusingUserNameAsync(u.UserName);
        }
        public Task<List<User?>> GetALLUsers()
        { 
            return UserRepository.GetALLAsync();
        }

        public Task<string> SaveUser(UsersRequest user)
        {
            User us = us = MappingUsers(user);
            return UserRepository.SaveUser(us);
        }

        public Task<bool> UpdateUser(UsersRequest user)
        {
            User us = MappingUsers(user);
            return UserRepository.UpdateUser(us);
        }
        public Task<bool> DeleteUser(string UserId)
        {
            return UserRepository.DeleteUser(UserId);
        }
        private User MappingUsers(UsersRequest user)
        {
            User us = new User();
            us.UserName = user.UserName;
            us.Email = user.Email;
            us.UserId = user.UserId;
            us.PhoneNumber = user.PhoneNumber;
            us.IsAdminUser = user.IsAdminUser;
            us.IsTenantUser = user.IsTenantUser;
            us.IsActive = user.IsActive;
            us.CreatedBy = user.CreatedBy;
            us.NotificationsEnabled = user.NotificationsEnabled;
            us.MfaEnabled = user.MfaEnabled;
            us.TenantId = user.TenantId;
            us.IsDeleted = user.IsDeleted;
            us.ModifiedBy = user.ModifiedBy;
            return us;
        }

        public Task<Apistatus> ResetPassword(string emailid)
        { 
            return UserRepository.ResetPassword(emailid);
        }

    }
}
