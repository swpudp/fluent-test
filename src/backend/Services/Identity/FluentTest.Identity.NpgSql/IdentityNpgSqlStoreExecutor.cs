using FluentTest.Identity.Stores;
using FluentTest.Infrastructure.NpgSql;
using Microsoft.Extensions.Configuration;

namespace FluentTest.Identity.NpgSql;

public class IdentityNpgSqlStoreExecutor(IConfiguration configuration) : AbstractNpgSqlStoreExecutor(configuration), IIdentityStoreExecutor
{
    protected override string ConnectionName => "Identity";
}
