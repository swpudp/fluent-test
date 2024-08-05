using FluentTest.Infrastructure.MsSql;
using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Configuration;

namespace FluentTest.Scheduled.MsSql;

public class ScheduledMsSqlStoreExecutor(IConfiguration configuration) : AbstractMsSqlStoreExecutor(configuration), IScheduledStoreExecutor
{
    protected override string ConnectionName => "Scheduled";
}
