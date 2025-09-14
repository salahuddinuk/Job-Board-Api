using JobBoardAPI.Dtos;
using System.Runtime.InteropServices.Marshalling;

namespace JobBoardAPI.Services
{
    public interface ICompanyService
    {
        Task<int> Create(CompanyDto company);

        Task<bool> Delete(int companyId);
        Task<IEnumerable<CompanyDto>> GetAll();
        Task<CompanyDto> GetById(int companyId);
        Task<CompanyDto> GetByName(string companyName);
        Task<bool> UpdateStatus(int companyId, bool isActive);

    }
}
