using JobBoardAPI.Dtos;

namespace JobBoardAPI.Services
{
    public interface IApplicationService
    {
        Task<int> Create(ApplicationDto application);
        Task<bool> Delete(int applicationId);
        Task<IEnumerable<ApplicationDto>> GetAll();
        Task<ApplicationDto> GetById(int applicationId);
        Task<IEnumerable<ApplicationDto>> GetAllByApplicantId(int applicantId);
        Task<bool> UpdateStatus(int applicationId, int status);
    }
}
