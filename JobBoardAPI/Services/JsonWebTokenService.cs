using JobBoardAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobBoardAPI.Services
{
    public class JsonWebTokenService : IJsonWebTokenService
    {
        private readonly ILogger<JsonWebTokenService> _logger;
        private readonly IConfiguration _configuration;
        public JsonWebTokenService(ILogger<JsonWebTokenService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public string Generate(User user) 
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("CreatedOn", user.CreatedOn.ToString("dd-MMM-yyyy")),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss fff"))
            };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpiryInMinutes"])),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception on JsonWebTokenService Generate Token >" + ex.Message);
                return "";
            }
        }
    }
}
