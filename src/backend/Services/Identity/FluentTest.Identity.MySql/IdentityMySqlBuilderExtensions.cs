using FluentTest.Identity.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Identity.MySql
{
    public static class IdentityMySqlBuilderExtensions
    {
        public static IdentityBuilder UseMySql(this IdentityBuilder builder)
        {
            builder.Services.AddScoped<IIdentityStoreExecutor, IdentityMySqlStoreExecutor>();
            return builder;
        }
    }
}