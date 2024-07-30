using FluentTest.Infrastructure.NpgSql;
using FluentTest.Scheduled.NpgSql.Stores;
using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Identity.NpgSql
{
    public static class ScheduledBuilderExtensions
    {
        public static IServiceCollection UseNpgSql(this IServiceCollection services)
        {
            services.AddScoped<IStoreExecutor, ScheduledNpgSqlStoreExecutor>();
            services.AddScoped<IJobLogStore, JobLogStore>();
            return services;
        }
    }
}