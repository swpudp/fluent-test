using Microsoft.AspNetCore.Mvc;

namespace FluentTest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevController : ControllerBase
    {
        private readonly ILogger _logger;

        public DevController(ILogger<DevController> logger)
        {
            _logger = logger;
        }

        [HttpGet("log")]
        public IActionResult Log()
        {
            _logger.LogInformation("���Լ�¼info������־");
            _logger.LogDebug("���Լ�¼debug������־");
            _logger.LogWarning("���Լ�¼Warning������־");
            _logger.LogError(new DivideByZeroException("����Ϊ0��"), "���Լ�¼error������־");
            return Ok("ok");
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            throw new NotSupportedException();
        }
    }
}