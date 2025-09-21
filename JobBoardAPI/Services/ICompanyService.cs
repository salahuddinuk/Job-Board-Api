using JobBoardAPI.Dtos;
using System.Runtime.InteropServices.Marshalling;

namespace JobBoardAPI.Services
{
    public interface ICompanyService
    {
        Task<int> CreateAsync(CompanyDto company);
        Task<bool> DeleteAsync(int companyId);
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<CompanyDto> GetByIdAsync(int companyId);
        Task<CompanyDto> GetByNameAsync(string companyName);
        Task<bool> UpdateStatusAsync(int companyId, bool isActive);
    }
}
