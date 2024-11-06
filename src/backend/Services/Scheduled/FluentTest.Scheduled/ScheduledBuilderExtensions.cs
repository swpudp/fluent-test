using FluentTest.Scheduled.Application;
using FluentTest.Scheduled.Service;
using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace FluentTest.Scheduled;

public static class ScheduledBuilderExtensions
{
    public static IMvcBuilder AddScheduledPart(this IMvcBuilder builder)
    {
        builder.AddApplicationPart(typeof(JobGroupController).Assembly);
        return builder;
    }

    public static ScheduledBuilder AddScheduledServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<QuartzOptions>(configuration.GetSection("Quartz"));
        services.Configure<QuartzOptions>(cfg =>
        {
            cfg.Scheduling.IgnoreDuplicates = true;
            cfg.Scheduling.OverWriteExistingData = true;
        });
        services.AddQuartz();
        services.AddQuartzHostedService(cfg =>
        {
            cfg.WaitForJobsToComplete = true;
            cfg.AwaitApplicationStarted = true;
        });
        services.AddScoped<IJobLogStore, JobLogStore>();
        services.AddScoped<JobManager>();
        return new ScheduledBuilder(services);
    }
}