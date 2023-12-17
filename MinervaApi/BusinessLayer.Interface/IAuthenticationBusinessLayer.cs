using Microsoft.AspNetCore.Mvc;
using Minerva.Models;

namespace Minerva.BusinessLayer.Interface
{
    
    public interface IAuthenticationBusinessLayer
    {
        public Task<AdminUser?>  authenticate(string username, string password);
    }
}