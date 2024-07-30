using Microsoft.Extensions.Configuration;

namespace FluentTest.Infrastructure.NpgSql
{
    public class IdentityNpgSqlStoreExecutor : AbstractNpgSqlStoreExecutor
    {
        protected IdentityNpgSqlStoreExecutor(IConfiguration configuration) : base(configuration) { }

        protected override string ConnectionName => "Identity";
    }
}
