using JobBoardAPI.Dtos;

namespace JobBoardAPI.Services
{
    public interface IApplicantService
    {
        Task<int> CreateAsync(ApplicantDto applicant);
        Task<bool> DeleteAsync(int applicantId);
        Task<IEnumerable<ApplicantDto>> GetAllAsync();
        Task<ApplicantDto> GetByIdAsync(int applicantId);
        Task<bool> UpdateStatusAsync(int applicantId, bool isActive);
    }
}
