using FluentTest.Infrastructure.NpgSql;
using FluentTest.Scheduled.NpgSql.Stores;
using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace FluentTest.Identity.NpgSql
{
    public static class ScheduledNpgSqlBuilderExtensions
    {
        public static IServiceCollection UseNpgSql(this IServiceCollection services)
        {
            services.AddQuartz(cfg =>
            {
                cfg.SchedulerId = "fluent-test-scheduler";
                cfg.InterruptJobsOnShutdown = true;
                cfg.InterruptJobsOnShutdownWithWait = true;
                cfg.UseSimpleTypeLoader();
                cfg.UsePersistentStore(p =>
                {
                    p.PerformSchemaValidation = true;
                    p.UseProperties = true;
                    p.RetryInterval = TimeSpan.FromSeconds(15);
                    p.UsePostgres(s =>
                    {
                        s.TablePrefix = "scheduled";
                        s.ConnectionStringName = "Scheduled";
                    });
                    p.UseClustering(c =>
                    {
                        c.CheckinMisfireThreshold = TimeSpan.FromSeconds(30);
                        c.CheckinInterval = TimeSpan.FromSeconds(15);
                    });
                });
                cfg.UseDefaultThreadPool();
            });
            services.AddScoped<IStoreExecutor, ScheduledNpgSqlStoreExecutor>();
            services.AddScoped<IJobLogStore, JobLogStore>();
            return services;
        }
    }
}