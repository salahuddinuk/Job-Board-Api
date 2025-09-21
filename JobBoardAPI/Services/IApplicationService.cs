using JobBoardAPI.Dtos;

namespace JobBoardAPI.Services
{
    public interface IApplicationService
    {
        Task<int> CreateAsync(ApplicationDto application);
        Task<bool> DeleteAsync(int applicationId);
        Task<IEnumerable<ApplicationDto>> GetAllAsync();
        Task<ApplicationDto> GetByIdAsync(int applicationId);
        Task<IEnumerable<ApplicationDto>> GetAllByApplicantIdAsync(int applicantId);
        Task<bool> UpdateStatusAsync(int applicationId, int status);
    }
}
