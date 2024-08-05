using FluentTest.Identity.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Identity.MsSql;

public static class IdentityMsSqlBuilderExtensions
{
    public static IdentityBuilder UseMsSql(this IdentityBuilder builder)
    {
        builder.Services.AddScoped<IIdentityStoreExecutor, IdentityMsSqlStoreExecutor>();
        return builder;
    }
}