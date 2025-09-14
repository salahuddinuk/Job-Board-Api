using JobBoardAPI.Dtos;

namespace JobBoardAPI.Services
{
    public interface IJobService
    {
        Task<int> Create(JobDto jobDto);
        Task<bool> Delete(int jobId);
        Task<JobDto> GetById(int jobId);
        Task<IEnumerable<JobDto>> GetAll();
        Task<IEnumerable<JobDto>> GetAllActive();
        Task<bool> UpdateStatus(int jobId, int Status);
    }
}
