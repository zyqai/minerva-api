namespace MinervaApi.Models
{
    public class Lender
    {
        public int? LenderID { get; set; }
        public int? TenantID { get; set; }
        public string? Name { get; set; }
        public string? ContactAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? LicensingDetails { get; set; }
        public string? CommercialMortgageProducts { get; set; }
        public decimal? InterestRates { get; set; }
        public string? Terms { get; set; }
        public decimal? LoanToValueRatio { get; set; }
        public string? ApplicationProcessDetails { get; set; }
        public string? UnderwritingGuidelines { get; set; }
        public decimal? ClosingCostsAndFees { get; set; }
        public string? SpecializedServices { get; set; }
    }
}
