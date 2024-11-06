using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Scheduled.MsSql;

public static class ScheduledMsSqlBuilderExtensions
{
    public static ScheduledBuilder UseMsSql(this ScheduledBuilder builder)
    {
        builder.Services.AddScoped<IScheduledStoreExecutor, ScheduledMsSqlStoreExecutor>();
        return builder;
    }
}