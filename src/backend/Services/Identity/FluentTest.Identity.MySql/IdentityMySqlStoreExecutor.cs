using FluentTest.Identity.Stores;
using FluentTest.Infrastructure.MySql;
using Microsoft.Extensions.Configuration;

namespace FluentTest.Identity.MySql;

public class IdentityMySqlStoreExecutor(IConfiguration configuration) : AbstractMySqlStoreExecutor(configuration), IIdentityStoreExecutor
{
    protected override string ConnectionName => "Identity";
}
