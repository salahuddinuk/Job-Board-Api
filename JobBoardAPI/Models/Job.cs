using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public sealed class Job
    {
        [Key]
        public int JobId { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? LastDate { get; set; }
        public int Status { get; set; }

        public Job(int jobId, int companyId, string title, string description, DateTime? activeFrom, DateTime? lastDate, int status)
        {
            JobId = jobId;
            CompanyId = companyId;
            Title = title;
            Description = description;
            CreatedOn = DateTime.Now;
            ActiveFrom = activeFrom;
            LastDate = lastDate;
            Status = status;
        }
    }
}
