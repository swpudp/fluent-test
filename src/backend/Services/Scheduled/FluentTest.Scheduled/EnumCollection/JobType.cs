using System.ComponentModel;

namespace FluentTest.Scheduled.EnumCollection;

public enum JobType
{
    /// <summary>
    /// http请求job
    /// </summary>
    [Description("http")]
    Http = 1,

    /// <summary>
    /// 中介者模式job
    /// </summary>
    [Description("MediatR")]
    Mediat = 2
}
