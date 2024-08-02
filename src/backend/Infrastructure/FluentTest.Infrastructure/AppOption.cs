namespace FluentTest.Infrastructure;

/// <summary>
/// 全局配置
/// </summary>
public class AppOption
{
    /// <summary>
    /// rsa私钥
    /// </summary>
    public string PrivateKey { get; set; }

    /// <summary>
    /// rsa公钥
    /// </summary>
    public string PublicKey { get; set; }
}
