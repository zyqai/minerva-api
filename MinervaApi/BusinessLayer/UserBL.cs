using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using MinervaApi.ExternalApi;

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

        public async Task<string> SaveUser(UsersRequest user)
        {
            User ?user1 = await UserRepository.GetuserusingUserNameAsync(user.CreatedBy);
            user.CreatedBy = user1?.UserId;
            User us = us = MappingUsers(user);
            return await UserRepository.SaveUser(us);
        }

        public async Task<bool> UpdateUser(UsersRequest user)
        {
            User? user1 = await UserRepository.GetuserusingUserNameAsync(user.ModifiedBy);
            user.ModifiedBy = user1?.UserId;
            User us = MappingUsers(user);
            return await UserRepository.UpdateUser(us);
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
            us.FirstName = user.FirstName;
            us.LastName = user.LastName;    
            us.Roles = user.Roles;
            return us;
        }
        public Task<List<User?>> GetTenantUserList(int tenantId)
        {
            return UserRepository.GetTenantUserList(tenantId);
        }
        public Task<APIStatus> Forgetpassword(string emailid)
        { 
            return UserRepository.Forgetpassword(emailid);
        }
        public Task<APIStatus> verifyemail(string emailid)
        {
            return UserRepository.verifyemail(emailid);
        }
    }
}
