using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Minerva.Models.Requests
{
    public class TenantRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TenantId { get; set; }

        [StringLength(255)]
        public string? TenantName { get; set; }

        [StringLength(255)]
        public string? TenantDomain { get; set; }

        [StringLength(255)]
        public string? TenantLogoPath { get; set; }

        [StringLength(1000)]
        public string? TenantAddress { get; set; }

        [StringLength(1000)]
        public string? TenantAddress1 { get; set; }

        [StringLength(45)]
        public string? TenantPhone { get; set; }

        [StringLength(45)]
        public string? TenantContactName { get; set; }

        [StringLength(225)]
        public string? TenantContactEmail { get; set; }

        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public int? stateid { get; set; }
    }
}
