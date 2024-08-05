using FluentTest.Identity.Stores;
using FluentTest.Infrastructure.MsSql;
using Microsoft.Extensions.Configuration;

namespace FluentTest.Identity.MsSql;

public class IdentityMsSqlStoreExecutor(IConfiguration configuration) : AbstractMsSqlStoreExecutor(configuration), IIdentityStoreExecutor
{
    protected override string ConnectionName => "Identity";
}
