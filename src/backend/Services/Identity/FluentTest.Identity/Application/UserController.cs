using FluentTest.Identity.Request;
using FluentTest.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenSsl.Crypto.Utility;
using System.Text;

namespace FluentTest.Identity.Application
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(UserManager<IdentityUser> userManager, IOptions<AppOption> appOption, SignInManager<IdentityUser> signInManager) : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IOptions<AppOption> _appOption = appOption;

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IdentityResult> CreateUserAsync([FromBody] AddUserRequest request)
        {
            byte[] keyBytes = Convert.FromBase64String(_appOption.Value.PrivateKey);
            byte[] cipherBytes = Convert.FromBase64String(request.Password);
            byte[] rawBytes = CryptoUtils.RsaDecrypt(keyBytes, cipherBytes, CipherMode.ECB, CipherPadding.PKCS1);
            string passwordRaw = Encoding.UTF8.GetString(rawBytes);
            IdentityUser user = new()
            {
                TenantId = ObjectId.GenerateNewId().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };
            IdentityResult result = await _userManager.CreateAsync(user, passwordRaw);
            return result;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IdentityResult> UpdateUserAsync([FromBody] IdentityUser user)
        {
            IdentityResult result = await _userManager.UpdateAsync(user);
            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IdentityResult> DeleteUserAsync([FromBody] IdentityUser user)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            return result;
        }

        /// <summary>
        /// 获取指定id的用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<IdentityUser> GetUserAsync(string userId)
        {
            IdentityUser result = await _userManager.FindByIdAsync(userId);
            return result;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<bool> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Username))
            {
                return false;
            }
            IdentityUser user = await _userManager.FindByNameAsync(loginRequest.Username);
            if (user == null)
            {
                return false;
            }
            bool locked = await _userManager.IsLockedOutAsync(user);
            if (locked)
            {
                return false;
            }

            bool success = await _userManager.CheckPasswordAsync(user, loginRequest.Password);


            //_userManager.SetAuthenticationTokenAsync();

            await _signInManager.SignInAsync(user, true);
            //_signInManager.SignInWithClaimsAsync()
            //_signInManager.TwoFactorSignInAsync();
            //_signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, success);
            //_signInManager.TwoFactorAuthenticatorSignInAsync()
            //_signInManager.ExternalLoginSignInAsync();
            //_signInManager.TwoFactorRecoveryCodeSignInAsync()

            return success;
        }
    }
}