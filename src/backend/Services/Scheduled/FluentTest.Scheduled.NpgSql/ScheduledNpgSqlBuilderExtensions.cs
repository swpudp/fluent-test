﻿using FluentTest.Infrastructure.NpgSql;
using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace FluentTest.Identity.NpgSql
{
    public static class ScheduledNpgSqlBuilderExtensions
    {
        public static IServiceCollection UseNpgSql(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services.Configure<QuartzOptions>(configurationSection);
            services.AddQuartz(cfg =>
            {
                cfg.SchedulerName = "flent_test_scheduler";
                cfg.SchedulerId = "flent_test_scheduler";
                cfg.InterruptJobsOnShutdown = true;
                cfg.InterruptJobsOnShutdownWithWait = true;
                cfg.UseSimpleTypeLoader();
                cfg.UsePersistentStore(p =>
                {
                    p.PerformSchemaValidation = false;
                    p.UseProperties = true;
                    p.RetryInterval = TimeSpan.FromSeconds(15);
                    p.UsePostgres(s =>
                    {
                        s.TablePrefix = "qrtz_";
                        s.ConnectionStringName = "Scheduled";
                    });
                    p.UseClustering(c =>
                    {
                        c.CheckinMisfireThreshold = TimeSpan.FromSeconds(30);
                        c.CheckinInterval = TimeSpan.FromSeconds(15);
                    });
                    p.UseSystemTextJsonSerializer();
                });
                cfg.UseDefaultThreadPool();
            });
            services.AddQuartzHostedService(cfg =>
            {
                cfg.WaitForJobsToComplete = true;
                cfg.AwaitApplicationStarted = true;
            });
            services.AddScoped<IScheduledStoreExecutor, ScheduledNpgSqlStoreExecutor>();
            return services;
        }
    }
}