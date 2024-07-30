using FluentTest.Infrastructure.NpgSql;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Identity.NpgSql
{
    public static class IdentityNpgSqlBuilderExtensions
    {
        public static IServiceCollection UseNpgSql(this IServiceCollection services)
        {
            services.AddScoped<IStoreExecutor, IdentityNpgSqlStoreExecutor>();
            return services;
        }
    }
}