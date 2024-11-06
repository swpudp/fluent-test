using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Scheduled.MySql;

public static class ScheduledMySqlBuilderExtensions
{
    public static ScheduledBuilder UseMySql(this ScheduledBuilder builder)
    {
        builder.Services.AddScoped<IScheduledStoreExecutor, ScheduledMySqlStoreExecutor>();
        return builder;
    }
}