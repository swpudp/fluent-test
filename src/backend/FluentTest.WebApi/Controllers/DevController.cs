using FluentTest.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenSsl.Crypto.Utility;
using System.Data;
using System.Reflection;
using System.Text;

namespace FluentTest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IOptions<AppOption> _appOption;
        private readonly IServiceProvider _serviceProvider;

        public DevController(ILogger<DevController> logger, IOptions<AppOption> appOption, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _appOption = appOption;
            _serviceProvider = serviceProvider;
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

        /// <summary>
        /// 生成rsa密钥对
        /// </summary>
        /// <returns></returns>
        [HttpGet("rsa-key")]
        public CipherKeyPair RsaKeyPair()
        {
            return RsaCertUtils.CreateCipherKeyPair();
        }

        [HttpGet("rsa-encrypt")]
        public string Encrypt(string raw)
        {
            byte[] cihperBytes = CryptoUtils.RsaEncrypt(_appOption.Value.PublicKey, raw, Encoding.UTF8, CipherMode.ECB, CipherPadding.PKCS1);
            return Convert.ToBase64String(cihperBytes);
        }

        [HttpPost("rsa-decrypt")]
        public string Decrypt([FromBody] string cipher)
        {
            byte[] cihperBytes = Convert.FromBase64String(cipher);
            byte[] privateKeyBytes = Convert.FromBase64String(_appOption.Value.PrivateKey);
            byte[] rawBytes = CryptoUtils.RsaDecrypt(privateKeyBytes, cihperBytes, CipherMode.ECB, CipherPadding.PKCS1);
            return Encoding.UTF8.GetString(rawBytes);
        }

        [HttpGet("invoke")]
        public bool TestInvoke()
        {
            Type type = Type.GetType("FluentTest.WebApi.Controllers.TestRepository");
            object service = _serviceProvider.GetService(type);

            MethodInfo method = type.GetMethod("Test");


            if(method.ReturnType.IsGenericType)
            {


                method.Invoke(service, null);


            }
            else
            {
                method.Invoke(service, null);
            }

          



            MethodInfo asyncMethod = type.GetMethod("TestAsync");
            asyncMethod.Invoke(service, null);


            return true;

        }
    }

    public class TestRepository : AbstractRepository
    {
        private readonly ILogger _logger;

        public TestRepository(ILogger<TestRepository> logger)
        {
            _logger = logger;
        }

        protected override IDbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }

        public Task Test()
        {
            _logger.LogInformation("反射执行的");
            return Task.CompletedTask;
        }

        public Task<bool> TestAsync()
        {
            return Task.FromResult(true);
        }
    }
}