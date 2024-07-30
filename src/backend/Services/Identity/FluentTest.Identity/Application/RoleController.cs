using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FluentTest.Identity.Application
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController(RoleManager<IdentityRole> roleManager) : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IdentityResult> CreateRoleAsync([FromBody] IdentityRole user)
        {
            IdentityResult result = await _roleManager.CreateAsync(user);
            return result;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IdentityResult> UpdateRoleAsync([FromBody] IdentityRole user)
        {
            IdentityResult result = await _roleManager.UpdateAsync(user);
            return result;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IdentityResult> DeleteRoleAsync([FromBody] IdentityRole user)
        {
            IdentityResult result = await _roleManager.DeleteAsync(user);
            return result;
        }
    }
}