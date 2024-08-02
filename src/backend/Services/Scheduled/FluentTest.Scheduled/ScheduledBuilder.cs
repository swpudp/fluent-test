using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Scheduled
{
    public class ScheduledBuilder(IServiceCollection services)
    {
        public IServiceCollection Services { get; } = services;
    }
}
