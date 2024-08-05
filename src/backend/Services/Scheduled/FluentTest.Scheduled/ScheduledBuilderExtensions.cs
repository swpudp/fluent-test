using FluentTest.Scheduled.Application;
using FluentTest.Scheduled.Service;
using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Scheduled;

public static class ScheduledBuilderExtensions
{
    public static IMvcBuilder AddScheduledPart(this IMvcBuilder builder)
    {
        builder.AddApplicationPart(typeof(JobGroupController).Assembly);
        return builder;
    }

    public static ScheduledBuilder AddScheduledServices(this IServiceCollection services)
    {
        services.AddScoped<IJobLogStore, JobLogStore>();
        services.AddScoped<JobManager>();
        return new ScheduledBuilder(services);
    }
}