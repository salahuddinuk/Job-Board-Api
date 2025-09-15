using JobBoardAPI.Dtos;
using JobBoardAPI.Models;
using JobBoardAPI.MQ;
using JobBoardAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJsonWebTokenService _tokenService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IUserService userService, IJsonWebTokenService tokenService, IMqSender mq, ILogger<LoginController> logger)
        {
            _userService = userService;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                User user = await _userService.Authenticate(login);

                if (user != null)
                {                   
                    return Ok(_tokenService.Generate(user));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}