﻿using System.ComponentModel.DataAnnotations;

namespace Minerva.Models.Requests
{
    public class ClientRequest
    {
      
        public int ClientId { get; set; }
        public string? UserId { get; set; }
        public int TenantId { get; set; }
        public string? ClientName { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateTime? dob { get; set; }
        public string? socialsecuritynumber { get; set; }
        public string? postalnumber { get; set; }
        public int? stateid { get; set; }
        public string? ClientAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? PreferredContact { get; set; }
        public string? CreditScore { get; set; }
        public string? LendabilityScore { get; set; }
        public int? MarriedStatus { get; set; }
        public int? SpouseClientId { get; set; }
        public string? RootFolder { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? City { get; set; }
        public string? ClientAddress1 { get; set; }
        public string? Gender { get; set; }
    }
}
