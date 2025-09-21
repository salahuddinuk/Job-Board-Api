using JobBoardAPI.Data;
using JobBoardAPI.Dtos;
using JobBoardAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace JobBoardAPI.Services
{
    public class JobService : IJobService
    {
        private readonly JobBoardDbContext _dbContext;
        private readonly ILogger<JobService> _logger;

        public JobService(JobBoardDbContext dbContext, ILogger<JobService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> CreateAsync(JobDto jobDto)
        {
            try
            {
                Job job = new Job(
                    jobDto.JobId,
                    jobDto.CompanyId,
                    jobDto.Title,
                    jobDto.Description,
                    jobDto.ActiveFrom,
                    jobDto.LastDate,
                    jobDto.Status
                    );

                await _dbContext.Jobs.AddAsync(job);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("CreateJob for id: " + jobDto.JobId);
                return job.JobId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on CreateJob > Exception: " + ex.Message);
                return 0;
            }
        
        }
    
        public async Task<bool> DeleteAsync(int jobId)
        {
            try
            {
                Job job = await _dbContext.Jobs.FindAsync(jobId);
                if (job != null)
                {
                    _dbContext.Jobs.Remove(job);
                    await _dbContext.SaveChangesAsync();

                    return true;
                }

                _logger.LogWarning("No job found for application id: " + jobId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on DeleteJob > Exception: " + ex.Message);
                return false;
            }
        }
        public async Task<JobDto> GetByIdAsync(int jobId)
        {
            try
            {
                Job job = await _dbContext.Jobs.FindAsync(jobId);

                return new JobDto(
                    job.JobId, 
                    job.CompanyId,
                    job.Title,
                    job.Description,
                    job.CreatedOn,
                    job.ActiveFrom,
                    job.LastDate,
                    job.Status
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetJob > Exception: " + ex.Message);
                return null;
            }
        }
        public async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Job> app = await _dbContext.Jobs.ToListAsync();

                IEnumerable<JobDto> appDtos = app.Select(c => new JobDto(
                    c.JobId,
                    c.CompanyId,
                    c.Title,
                    c.Description,
                    c.CreatedOn,
                    c.ActiveFrom,
                    c.LastDate,
                    c.Status
                ));

                return appDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetAllJobs > Exception: " + ex.Message);
                return null;
            }
        }
        public async Task<IEnumerable<JobDto>> GetAllActiveAsync()
        {
            try
            {
                IEnumerable<Job> app = await _dbContext.Jobs.Where(c => c.Status==1).ToListAsync();

                IEnumerable<JobDto> appDtos = app.Select(c => new JobDto(
                    c.JobId,
                    c.CompanyId,
                    c.Title,
                    c.Description,
                    c.CreatedOn,
                    c.ActiveFrom,
                    c.LastDate,
                    c.Status
                ));

                return appDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetAllActiveJobs > Exception: " + ex.Message);
                return null;
            }
        }
        public async Task<bool> UpdateStatusAsync(int jobId, int status)
        {
            try
            {
                Job job = await _dbContext.Jobs.FindAsync(jobId);

                if (job != null)
                {
                    job.Status = status;
                    _dbContext.Jobs.Update(job);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on UpdateStatus > Exception: " + ex.Message);
                return false;
            }
        }
    }
}
