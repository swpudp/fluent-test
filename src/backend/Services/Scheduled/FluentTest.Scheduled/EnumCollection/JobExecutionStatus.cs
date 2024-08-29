using System.ComponentModel;

namespace FluentTest.Scheduled.EnumCollection;

public enum JobExecutionStatus
{
    [Description("成功")]
    Success = 1,

    [Description("错误")]
    Error = 99,
}
