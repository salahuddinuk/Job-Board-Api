using JobBoardAPI.Data;
using JobBoardAPI.Dtos;
using JobBoardAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobBoardAPI.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly JobBoardDbContext _dbContext;
        private readonly ILogger<ApplicantService> _logger;
        public ApplicantService(JobBoardDbContext context, ILogger<ApplicantService> logger) {
            _dbContext = context;
            _logger = logger;
        }
        public async Task<int> CreateAsync(ApplicantDto applicantDto)
        {
            Applicant applicant = new Applicant(0, applicantDto.Name, applicantDto.Email, applicantDto.IsActive);

            await _dbContext.Applicants.AddAsync(applicant);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("CreateApplicant with Id: " + applicant.ApplicantId);
            return applicant.ApplicantId;
        }
        public async Task<bool> DeleteAsync(int applicantId)
        {
            try
            {
                Applicant applicant = await _dbContext.Applicants.FindAsync(applicantId);
                _dbContext.Applicants.Remove(applicant);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("DeleteApplicant with Id: " + applicant.ApplicantId);
                return true;
            }
            catch (Exception ex) {
                _logger.LogError("Exception on DeleteApplicant for applicant id: " + applicantId + " > Exception: " + ex.Message);
                return false;
            }
        }
        public async Task<IEnumerable<ApplicantDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Applicant> app = await _dbContext.Applicants.ToListAsync();
                
                IEnumerable<ApplicantDto> appDtos = app.Select(c => new ApplicantDto(
                   c.ApplicantId,
                   c.Name,
                   c.Email,
                   c.IsActive
               ));

                return appDtos;

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetAllApplicants > Exception: " + ex.Message);
                return null;
            }
        }
        public async Task<ApplicantDto> GetByIdAsync(int applicantId)
        {
            try
            {
                Applicant applicant = await _dbContext.Applicants.FindAsync(applicantId);

                return new ApplicantDto(applicant.ApplicantId, applicant.Name, applicant.Email, applicant.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetApplicantbyId for id: " + applicantId + " > Exception: " + ex.Message);
                return null;
            }
        }
        public async Task<bool> UpdateStatusAsync(int  applicantId, bool status)
        {
            try
            {
                Applicant applicant = await _dbContext.Applicants.FindAsync(applicantId);
                if (applicant != null)
                {
                    applicant.IsActive = status;
                    _dbContext.Applicants.Update(applicant);
                    await _dbContext.SaveChangesAsync();

                    return true;
                }
                _logger.LogWarning("UpdateStatus applicant not found for Id: " + applicant.ApplicantId);
                return false;
                    
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on UpdateStatus for id: " + applicantId + " > Exception: " + ex.Message);
                return false;
            }

        }
    }
}
