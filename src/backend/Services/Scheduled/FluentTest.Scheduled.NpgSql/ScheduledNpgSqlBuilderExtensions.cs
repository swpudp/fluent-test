using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Scheduled.NpgSql
{
    public static class ScheduledNpgSqlBuilderExtensions
    {
        public static ScheduledBuilder UseNpgSql(this ScheduledBuilder builder)
        {
            builder.Services.AddScoped<IScheduledStoreExecutor, ScheduledNpgSqlStoreExecutor>();
            return builder;
        }
    }
}