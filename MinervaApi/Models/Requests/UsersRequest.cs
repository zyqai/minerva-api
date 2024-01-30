using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Minerva.Models.Requests
{
    public class UsersRequest
    {
        public string? UserId { get; set; }

        public int? TenantId { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        
        public DateTime? CreateTime { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedTime { get; set; } = DateTime.UtcNow;

        [StringLength(36)]
        public string? CreatedBy { get; set; }

        [StringLength(36)]
        public string? ModifiedBy { get; set; }

        [StringLength(45)]
        public string? PhoneNumber { get; set; }

        public bool? NotificationsEnabled { get; set; }

        public bool? MfaEnabled { get; set; }

        public int? IsTenantUser { get; set; }

        public int? IsAdminUser { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Roles { get; set; }

        //public Tenant Tenant { get; set; }
    }
}
