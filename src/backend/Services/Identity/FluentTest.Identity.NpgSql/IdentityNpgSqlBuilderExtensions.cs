using FluentTest.Identity.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Identity.NpgSql;

public static class IdentityNpgSqlBuilderExtensions
{
    public static IdentityBuilder UseNpgSql(this IdentityBuilder builder)
    {
        builder.Services.AddScoped<IIdentityStoreExecutor, IdentityNpgSqlStoreExecutor>();
        return builder;
    }
}