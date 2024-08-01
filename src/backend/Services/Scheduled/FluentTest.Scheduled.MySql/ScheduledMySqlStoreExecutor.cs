using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Configuration;
using FluentTest.Infrastructure.MySql;

namespace FluentTest.Scheduled.MySql
{
    public class ScheduledMySqlStoreExecutor(IConfiguration configuration) : AbstractMySqlStoreExecutor(configuration), IScheduledStoreExecutor
    {
        protected override string ConnectionName => "Scheduled";
    }
}
