using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.Scheduled.Jobs;

public class MediatJob(IJobLogStore jobLogStore, ILogger<HttpJob> logger) : AbstractJob(jobLogStore, logger)
{
    public override Task DoExecute(IJobExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
