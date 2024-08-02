using Dapper.Extensions.Expression;
using FluentTest.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace FluentTest.Identity;

[TableNaming(NamingPolicy.SnakeCase), FieldNaming(NamingPolicy.SnakeCase)]
public class IdentityRole : IdentityRole<string>, ITenantEntity<string>
{
    public string Code { get; set; }

    public string TenantId { get; set; }
}


[TableNaming(NamingPolicy.SnakeCase), FieldNaming(NamingPolicy.SnakeCase)]
public class IdentityUserRole : IdentityUserRole<string>, ITenantEntity<string>
{
    public string TenantId { get; set; }
}

[TableNaming(NamingPolicy.SnakeCase), FieldNaming(NamingPolicy.SnakeCase)]
public class IdentityRoleClaim : IdentityRoleClaim<string>, ITenantEntity<string>
{
    public string TenantId { get; set; }
}