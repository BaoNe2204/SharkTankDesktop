using System;

namespace SharkTank.Core.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string BusinessLicense { get; set; }
        public string LogoPath { get; set; }
        public string RepresentativeName { get; set; }
        public string RepresentativePosition { get; set; }
        public string Hotline { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
