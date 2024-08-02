namespace FluentTest.Identity.Request;

/// <summary>
/// 添加用户请求
/// </summary>
public class AddUserRequest
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 邮件
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 电话号码
    /// </summary>
    public string PhoneNumber { get; set; }
}
