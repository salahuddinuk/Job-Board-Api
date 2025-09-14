using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public sealed class Application
    {
        [Key]
        public int ApplicationId { get; set; }
        public int ApplicantId {  get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }

        public Application(int applicationId, int applicantId, int jobId, int status) { 
            ApplicationId = applicationId;
            ApplicantId = applicantId;            
            JobId = jobId;
            Status = status;
        }
     }
    
}
