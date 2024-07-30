using Dapper.Extensions.Expression;
using FluentTest.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace FluentTest.Identity
{
    [TableNaming(NamingPolicy.SnakeCase), FieldNaming(NamingPolicy.SnakeCase)]
    public class IdentityUser : IdentityUser<string>, ITenantEntity<string>
    {
        public string TenantId { get; set; }
    }

    [TableNaming(NamingPolicy.SnakeCase), FieldNaming(NamingPolicy.SnakeCase)]
    public class IdentityUserClaim : IdentityUserClaim<string>, ITenantEntity<string>
    {
        public string TenantId { get; set; }
    }


    [TableNaming(NamingPolicy.SnakeCase), FieldNaming(NamingPolicy.SnakeCase)]
    public class IdentityUserLogin : IdentityUserLogin<string>, ITenantEntity<string>
    {
        public string TenantId { get; set; }
    }


    [TableNaming(NamingPolicy.SnakeCase), FieldNaming(NamingPolicy.SnakeCase)]
    public class IdentityUserToken : IdentityUserToken<string>, ITenantEntity<string>
    {
        public string TenantId { get; set; }
    }
}