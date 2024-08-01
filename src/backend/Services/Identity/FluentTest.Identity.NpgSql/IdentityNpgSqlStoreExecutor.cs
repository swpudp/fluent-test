using FluentTest.Identity.Stores;
using Microsoft.Extensions.Configuration;

namespace FluentTest.Infrastructure.NpgSql
{
    public class IdentityNpgSqlStoreExecutor(IConfiguration configuration) : AbstractNpgSqlStoreExecutor(configuration), IIdentityStoreExecutor
    {
        protected override string ConnectionName => "Identity";
    }
}
