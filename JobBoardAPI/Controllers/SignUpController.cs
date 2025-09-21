using JobBoardAPI.Dtos;
using JobBoardAPI.Models;
using JobBoardAPI.MQ;
using JobBoardAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJsonWebTokenService _tokenService;
        private readonly ILogger<LoginController> _logger;

        public SignUpController(IUserService userService, IJsonWebTokenService tokenService, IMqSender mq, ILogger<LoginController> logger)
        {
            _userService = userService;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserDto user)
        {
            try
            {         
                bool isCreated = await _userService.CreateAsync(user);
                if (isCreated)
                {                   
                    return Ok(true);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}