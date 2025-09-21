using JobBoardAPI.Dtos;

namespace JobBoardAPI.Services
{
    public interface IJobService
    {
        Task<int> CreateAsync(JobDto jobDto);
        Task<bool> DeleteAsync(int jobId);
        Task<JobDto> GetByIdAsync(int jobId);
        Task<IEnumerable<JobDto>> GetAllAsync();
        Task<IEnumerable<JobDto>> GetAllActiveAsync();
        Task<bool> UpdateStatusAsync(int jobId, int Status);
    }
}
