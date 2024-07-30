using FluentTest.Scheduled.Application;
using FluentTest.Scheduled.Service;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Identity
{
    public static class ScheduledBuilderExtensions
    {
        public static IMvcBuilder AddScheduledPart(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(JobGroupController).Assembly);
            return builder;
        }

        public static IServiceCollection AddScheduledServices(this IServiceCollection services, Action<IServiceCollection> action)
        {
            action(services);
            services.AddScoped<JobManager>();
            return services;
        }
    }
}