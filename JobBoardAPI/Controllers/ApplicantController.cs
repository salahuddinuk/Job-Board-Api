using JobBoardAPI.Dtos;
using JobBoardAPI.MQ;
using JobBoardAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly IMqSender _mq;
        private readonly ILogger<ApplicantController> _logger;

        public ApplicantController(IApplicantService applicantService, IMqSender mq, ILogger<ApplicantController> logger)
        {
            _applicantService = applicantService;
            _mq = mq;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _applicantService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _applicantService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicantDto applicantDto)
        {
            try
            {
                var data = System.Text.Json.JsonSerializer.Serialize(applicantDto);
                _logger.LogInformation("Create request received with data: " + data);

                bool isSent=await _mq.SendMessageAsync(data);
                return Ok(isSent);

                //return Ok(await _applicantService.Create(applicantDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] StatusBoolDto status)
        {
            try
            {
                return Ok(await _applicantService.UpdateStatus(status.Id, status.Status));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int applicantId)
        {
            try
            {
                return Ok(await _applicantService.Delete(applicantId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}