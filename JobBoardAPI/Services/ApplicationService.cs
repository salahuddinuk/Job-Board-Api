using JobBoardAPI.Data;
using JobBoardAPI.Dtos;
using JobBoardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobBoardAPI.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly JobBoardDbContext _dbContext;
        private readonly ILogger<ApplicationService> _logger;

        public ApplicationService(JobBoardDbContext context, ILogger<ApplicationService> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<int> Create(ApplicationDto applicationDto)
        {
            try
            {
                Application app = new Application(
                    0,
                    applicationDto.ApplicantId,
                    applicationDto.JobId,
                    0
                    );

                await _dbContext.Applications.AddAsync(app);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("CreateApplication for id: " + app.ApplicationId);
                return app.ApplicationId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on CreateApplication > Exception: " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> Delete(int applicationId)
        {
            try
            {
                Application app= await _dbContext.Applications.FindAsync(applicationId);
                if (app != null)
                {
                    _dbContext.Applications.Remove(app);
                    await _dbContext.SaveChangesAsync();

                    return true;
                }

                _logger.LogWarning("No application found for application id: " + applicationId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on DeleteApplication > Exception: " + ex.Message);
                return false;
            }
        }
        public async Task<IEnumerable<ApplicationDto>> GetAll()
        {
            try
            {
                IEnumerable<Application> app = await _dbContext.Applications.ToListAsync();

                IEnumerable<ApplicationDto> appDtos = app.Select(c => new ApplicationDto(                   
                    c.ApplicationId,
                    c.ApplicantId,
                    c.JobId,
                    c.Status
                ));

                return appDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetAllApplication > Exception: " + ex.Message);
                return null;
            }
        }
        public async Task<ApplicationDto> GetById(int applicationId)
        {
            try
            {
                Application app = await _dbContext.Applications.FindAsync(applicationId);
              
                return new ApplicationDto(app.ApplicationId, app.ApplicantId, app.JobId, app.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetApplicationById > Exception: " + ex.Message);
                return null;
            }
        }
        public async Task<IEnumerable<ApplicationDto>> GetAllByApplicantId(int applicantId)
        {
            try
            {
                IEnumerable<Application> app = await _dbContext.Applications.Where(c => c.ApplicantId == applicantId).ToListAsync();

                IEnumerable<ApplicationDto> appDtos = app.Select(c => new ApplicationDto(
                                    c.ApplicationId,
                                    c.ApplicantId,
                                    c.JobId,
                                    c.Status
                                ));

                return appDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetApplicationById > Exception: " + ex.Message);
                return null;
            }
        }
        public async Task<bool> UpdateStatus(int applicationId, int status)
        {
            try
            {
                Application app = await _dbContext.Applications.FindAsync(applicationId);

                if (app != null)
                {
                    app.Status = status;
                    _dbContext.Applications.Update(app);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetApplicationById > Exception: " + ex.Message);
                return false;
            }
        }
    }
}
