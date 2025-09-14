using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public sealed class Applicant
    {
        public int ApplicantId { get; set; }
        public string Name {  get; set; }
        public string Email { get; set; }
        public bool IsActive {  get; set; }

        public Applicant(int applicantId, string name, string email, bool isActive) {
            ApplicantId = applicantId;
            Name = name;
            Email = email;
            IsActive = isActive;
        }
    }         
}
