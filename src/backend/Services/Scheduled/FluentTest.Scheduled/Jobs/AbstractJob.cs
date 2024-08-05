using FluentTest.Infrastructure;
using FluentTest.Scheduled.EnumCollection;
using FluentTest.Scheduled.Model;
using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentTest.Scheduled.Jobs;

public abstract class AbstractJob(IJobLogStore jobLogStore, ILogger logger) : IJob
{
    private readonly ILogger _logger = logger;
    private readonly IJobLogStore _jobLogStore = jobLogStore;
    protected readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

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
        return Task.CompletedTask;
    }

    private async Task AfterExecute(IJobExecutionContext context)
    {
        await CreateJobLog(context);
    }

    private async Task CreateJobLog(IJobExecutionContext context)
    {
        JobLog log = new JobLog
        {
            Id = ObjectId.GenerateNewId().ToString(),
            JobName = context.JobDetail.Key.Name,
            JobGroup = context.JobDetail.Key.Group,
            CreateTime = DateTime.Now,
            CreatorId = "job",
            CreatorName = "job",
            StartTime = new DateTime(context.FireTimeUtc.Ticks),
            EndTime = DateTime.Now,
            Duration = context.JobRunTime.TotalMilliseconds
        };
        if (context.MergedJobDataMap.TryGetString("tenantId", out string tenantId))
        {
            log.TenantId = tenantId;
        }
        object? exObj = context.Get("ex");
        if (exObj is Exception e)
        {
            log.JobStatus = JobExecutionStatus.Error;
            log.FailReason = e?.ToString();
        }
        else
        {
            log.JobStatus = JobExecutionStatus.Success;
        }
        await _jobLogStore.CreateAsync(log);
    }
}
