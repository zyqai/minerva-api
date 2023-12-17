using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;

namespace Minerva.Controllers
{
    [ApiController]
    public class Authentication: ControllerBase
    {
        IAuthenticationBusinessLayer authentication;
        public Authentication(IAuthenticationBusinessLayer _authentication)
        {
            this.authentication = _authentication;
        }

        [HttpGet]
        [Route("/auth")]
        public string GetHealth()
        {
            return "Hello World";
        }

        [HttpPost]
        [Route("/auth")]
        public Task<AdminUser?>  AuthenticateUser([FromBody] Models.Requests.AuthUserRequest userRequest)
        {
            return authentication.authenticate(userRequest.UserName, userRequest.Password);
        }
    }
}
