using System.ComponentModel.DataAnnotations;

namespace Minerva.Models.Requests
{
     public class AuthUserRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}