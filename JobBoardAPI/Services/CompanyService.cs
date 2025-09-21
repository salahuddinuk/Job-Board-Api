using JobBoardAPI.Data;
using JobBoardAPI.Dtos;
using JobBoardAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace JobBoardAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly JobBoardDbContext _dbContext;
        private readonly ILogger<CompanyService> _logger;
        public CompanyService(JobBoardDbContext context, ILogger<CompanyService> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<int> CreateAsync(CompanyDto companyDto)
        {
            try
            {
                Company company = new Company(0, companyDto.Name, companyDto.Address, true);
                await _dbContext.Companies.AddAsync(company);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("CreateCompany for id: " + company.CompanyId);
                return company.CompanyId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on CreateCompany > Exception: " + ex.Message);
                return 0;
            }
        }
        public async Task<bool> DeleteAsync(int companyId)
        {
            try
            {
                Company company = await _dbContext.Companies.FindAsync(companyId);
                if (company != null)
                {
                    _dbContext.Companies.Remove(company);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on DeleteCompany for company id: " + companyId + " > Exception: " + ex.Message);
                return false;
            }
        }
        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Company> app = await _dbContext.Companies.ToListAsync();

                IEnumerable<CompanyDto> appDtos = app.Select(c => new CompanyDto(
                   c.CompanyId,
                   c.Name,
                   c.Address,
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
        public async Task<CompanyDto> GetByIdAsync(int companyId)
        {
            try
            {
                Company company = await _dbContext.Companies.FindAsync(companyId);
                if (company != null)
                    return new CompanyDto(company.CompanyId, company.Name, company.Address, company.IsActive);

                _logger.LogWarning("No company fuond for id: " + companyId);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetCompanyById for company id: " + companyId + " > Exception: " + ex.Message);
                return null;
            }
        }
        public async Task<CompanyDto> GetByNameAsync(string companyName)
        {
            try
            {
                Company company = await _dbContext.Companies.Where(c => c.Name == companyName).FirstOrDefaultAsync();

                if (company != null)
                    return new CompanyDto(company.CompanyId, company.Name, company.Address, company.IsActive);

                _logger.LogWarning("No company fuond for name: " + companyName);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on GetCompanyByName for company name: " + companyName + " > Exception: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateStatusAsync(int companyId, bool isActive)
        {
            try
            {
                Company company = await _dbContext.Companies.FindAsync(companyId);
                if (company != null)
                {
                    company.IsActive = isActive;
                    _dbContext.Companies.Update(company);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                _logger.LogWarning("No company fuond for id: " + companyId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on UpdateStatus for id: " + companyId + " > Exception: " + ex.Message);
                return false;
            }
        }
    }
}
