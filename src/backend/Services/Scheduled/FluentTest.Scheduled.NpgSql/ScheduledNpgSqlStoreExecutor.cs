using FluentTest.Infrastructure.NpgSql;
using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Configuration;

namespace FluentTest.Scheduled.NpgSql;

public class ScheduledNpgSqlStoreExecutor(IConfiguration configuration) : AbstractNpgSqlStoreExecutor(configuration), IScheduledStoreExecutor
{
    protected override string ConnectionName => "Scheduled";
}
