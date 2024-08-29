﻿using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace FluentTest.Scheduled.MsSql;

public static class ScheduledMsSqlBuilderExtensions
{
    public static ScheduledBuilder UseMsSql(this ScheduledBuilder builder, IConfigurationSection configurationSection)
    {
        builder.Services.Configure<QuartzOptions>(configurationSection);
        builder.Services.AddQuartz(cfg =>
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
                p.UseSqlServer(s =>
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
        builder.Services.AddQuartzHostedService(cfg =>
        {
            cfg.WaitForJobsToComplete = true;
            cfg.AwaitApplicationStarted = true;
        });
        builder.Services.AddScoped<IScheduledStoreExecutor, ScheduledMsSqlStoreExecutor>();
        return builder;
    }
}