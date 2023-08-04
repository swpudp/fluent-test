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
            _logger.LogInformation("测试记录info级别日志");
            _logger.LogDebug("测试记录debug级别日志");
            _logger.LogWarning("测试记录Warning级别日志");
            _logger.LogError(new DivideByZeroException("除数为0啦"), "测试记录error级别日志");
            return Ok("ok");
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            throw new NotSupportedException();
        }
    }
}