using JobBoardAPI.Dtos;
using JobBoardAPI.Models;

namespace JobBoardAPI.Services
{
    public interface IUserService
    {
        Task<bool> Create(UserDto user);
        Task<Dictionary<bool, string>> UpdatePassword(UserPasswordDto userPassword);
        Task<bool> UpdateStatus(int userId, bool isActive);
        Task<User> Authenticate(LoginDto login);
        
    }
}
