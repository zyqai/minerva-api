using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;

namespace Minerva.BusinessLayer
{
    
    public class AuthenticationBusinessLayer: IAuthenticationBusinessLayer
    {
        IAdminUserRepository adminUserRepository;
        public AuthenticationBusinessLayer(IAdminUserRepository _adminUserRepository) {
            this.adminUserRepository = _adminUserRepository;
        }
        public Task<AdminUser?>  authenticate(String username, String password) {
            return adminUserRepository.FindUserPassword(username, password);
        }
    }
}