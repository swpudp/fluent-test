using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.Scheduled.Jobs
{
    public class MediatJob : AbstractJob
    {
        public MediatJob(ILogger<MediatJob> logger) : base(logger)
        {
        }

        public override Task DoExecute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
