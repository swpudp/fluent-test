using FluentTest.Infrastructure;
using FluentTest.Scheduled.EnumCollection;
using FluentTest.Scheduled.Model;
using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FluentTest.Scheduled.Jobs
{
    public abstract class AbstractJob : IJob
    {
        private readonly ILogger _logger;
        private readonly IJobLogStore _jobLogStore;

        protected AbstractJob(ILogger logger, IJobLogStore jobLogStore)
        {
            _logger = logger;
            _jobLogStore = jobLogStore;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await BeforeExecute(context);
            try
            {
                await DoExecute(context);
            }
            catch (Exception ex)
            {
                context.Put("ex", ex);
                _logger.LogError(ex, ex.Message);
            }
            await AfterExecute(context);
        }

        public abstract Task DoExecute(IJobExecutionContext context);

        private Task BeforeExecute(IJobExecutionContext context)
        {
            context.Put("executeStart", DateTime.Now.Ticks);
            return Task.CompletedTask;
        }

        private async Task AfterExecute(IJobExecutionContext context)
        {
            await CreateJobLog(context);
        }

        private async Task CreateJobLog(IJobExecutionContext context)
        {
            JobLog log = new JobLog();
            log.Id = ObjectId.GenerateNewId().ToString();
            log.TenantId = context.Get("tenantId")?.ToString();
            log.JobName = context.JobDetail.Key.Name;
            log.CreateTime = DateTime.Now;
            log.CreatorId = "job";
            log.CreatorName = "job";
            if (long.TryParse(context.Get("executeStart")?.ToString(), out long executeStart))
            {
                log.StartTime = new DateTime(executeStart);
                log.EndTime = DateTime.Now;
                log.Duration = DateTime.Now.Ticks - executeStart;
            }
            object? exObj = context.Get("ex");
            if (exObj is Exception e)
            {
                log.JobStatus = JobExecutionStatus.Error;
                log.FailReason = e?.Message;
            }
            else
            {
                log.JobStatus = JobExecutionStatus.Success;
            }
            await _jobLogStore.CreateAsync(log);
        }
    }
}
