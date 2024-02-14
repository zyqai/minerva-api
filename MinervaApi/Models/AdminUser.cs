using System.ComponentModel.DataAnnotations;

namespace Minerva.Models
{
     public class AdminUser
    {
        public string AdminUserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}