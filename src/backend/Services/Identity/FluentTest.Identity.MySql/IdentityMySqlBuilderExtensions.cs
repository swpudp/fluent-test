using FluentTest.Identity.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Identity.MySql
{
    public static class IdentityMySqlBuilderExtensions
    {
        public static IServiceCollection UseMySql(this IServiceCollection services)
        {
            services.AddScoped<IIdentityStoreExecutor, IdentityMySqlStoreExecutor>();
            return services;
        }
    }
}