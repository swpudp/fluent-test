using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.Scheduled.EnumCollection
{
    public enum JobExecutionStatus
    {
        [Description("成功")]
        Success = 1,

        [Description("错误")]
        Error = 99,
    }
}
