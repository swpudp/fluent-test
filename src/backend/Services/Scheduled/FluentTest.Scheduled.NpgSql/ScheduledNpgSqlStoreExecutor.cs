using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Configuration;

namespace FluentTest.Infrastructure.NpgSql
{
    public class ScheduledNpgSqlStoreExecutor(IConfiguration configuration) : AbstractNpgSqlStoreExecutor(configuration), IScheduledStoreExecutor
    {
        protected override string ConnectionName => "Scheduled";
    }
}
