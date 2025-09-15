using JobBoardAPI.Dtos;
using JobBoardAPI.Models;

namespace JobBoardAPI.Services
{
    public interface IJsonWebTokenService
    {
        string Generate(User user); 
    }
}
