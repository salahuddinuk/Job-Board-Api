using JobBoardAPI.Dtos;

namespace JobBoardAPI.Services
{
    public interface IApplicantService
    {
        Task<int> Create(ApplicantDto applicant);
        Task<bool> Delete(int applicantId);
        Task<IEnumerable<ApplicantDto>> GetAll();
        Task<ApplicantDto> GetById(int applicantId);
        Task<bool> UpdateStatus(int applicantId, bool isActive);
    }
}
