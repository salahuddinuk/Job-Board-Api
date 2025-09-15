using JobBoardAPI.Data;
using JobBoardAPI.Dtos;
using JobBoardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobBoardAPI.Services
{
    public class UserService : IUserService
    {
        private readonly JobBoardDbContext _dbContext;
        private readonly ILogger<UserService> _logger;
        public UserService(JobBoardDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> Create(UserDto userDto)
        {
            try
            {
                await _dbContext.Users.AddAsync(new User(0, userDto.Email, userDto.Password, userDto.IsActive));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Dictionary<bool, string>> UpdatePassword(UserPasswordDto dto)
        {
            Dictionary<bool, string> resultValue = new Dictionary<bool, string>();
            try
            {
                User user = await _dbContext.Users.FindAsync(dto.id);

                if (user != null)
                {
                    if (user.Password != dto.OldPassword)
                    {
                        resultValue.Add(true, "Old password does not match");
                        return resultValue;
                    }

                    user.Password = dto.NewPassword;
                    _dbContext.Users.Update(user);
                    await _dbContext.SaveChangesAsync();
                    resultValue.Add(true, "Password updated");
                    return resultValue;
                }
                resultValue.Add(false, "User not found");
                return resultValue;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on UpdatePassword > " + ex.Message);
                resultValue.Add(false, "Unable to update password");
                return resultValue;
            }
        }
        public async Task<bool> UpdateStatus(int id, bool isActive)
        {
            try
            {
                User user = await _dbContext.Users.FindAsync(id);

                if (user != null)
                {
                    _dbContext.Update(user);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<User> Authenticate(LoginDto loginDto)
        {
            try
            {
                User user = await _dbContext.Users.Where(c => c.Email.ToLower()==loginDto.Email.ToLower() && c.Password==loginDto.Password).FirstOrDefaultAsync();

                if (user != null)
                {
                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
