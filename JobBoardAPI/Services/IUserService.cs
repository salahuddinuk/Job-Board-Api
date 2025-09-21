using JobBoardAPI.Dtos;
using JobBoardAPI.Models;

namespace JobBoardAPI.Services
{
    public interface IUserService
    {
        Task<bool> CreateAsync(UserDto user);
        Task<Dictionary<bool, string>> UpdatePasswordAsync(UserPasswordDto userPassword);
        Task<bool> UpdateStatusAsync(int userId, bool isActive);
        Task<User> AuthenticateAsync(LoginDto login);
        
    }
}
