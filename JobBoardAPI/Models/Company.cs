using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public sealed class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    
        public Company(int companyId, string name, string address, bool isActive)
        {
            CompanyId =companyId;
            Name = name;
            Address = address;
            IsActive = isActive;
        }
    }
}
