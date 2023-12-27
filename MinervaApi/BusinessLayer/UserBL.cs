using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;

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
        public Task<List<User?>> GetALLUsers()
        { 
            return UserRepository.GetALLAsync();
        }

        public bool SaveUser(UsersRequest user)
        {
            User us = us = MappingUsers(user);
            return UserRepository.SaveUser(us);
        }

        public bool UpdateUser(UsersRequest user)
        {
            User us = MappingUsers(user);
            return UserRepository.UpdateUser(us);
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
    }
}
