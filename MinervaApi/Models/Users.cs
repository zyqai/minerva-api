using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Minerva.Models
{
    public class User
    {
       
        [StringLength(45)]
        public string? UserId { get; set; }

        public int? TenantId { get; set; }

        [Required]
        [StringLength(16)]
        public string? UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        [Required]
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

        [ForeignKey("Tenant")]
        public Tenant Tenant { get; set; }
    }

}
