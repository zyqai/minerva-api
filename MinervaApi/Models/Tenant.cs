using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Minerva.Models
{
    public class Tenant
    {
        public int TenantId { get; set; }
        public string? TenantName { get; set; }
        public string? tenantDescription { get; set; }
        public string? TenantDomain { get; set; }
        public string? TenantLogoPath { get; set; }
        public string? TenantAddress { get; set; }
        public string? TenantAddress1 { get; set; }
        public string? TenantPhone { get; set; }
        public string? TenantContactName { get; set; }
        public string? TenantContactEmail { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public int? stateid { get; set; }
        public string? CreatedBY { get; set; }
        public string? UpdatedBY { get; set; }
    }
}
