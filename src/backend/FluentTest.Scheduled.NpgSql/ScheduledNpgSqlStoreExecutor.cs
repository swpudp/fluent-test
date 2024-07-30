using Microsoft.Extensions.Configuration;

namespace FluentTest.Infrastructure.NpgSql
{
    public class ScheduledNpgSqlStoreExecutor : AbstractNpgSqlStoreExecutor
    {
        protected ScheduledNpgSqlStoreExecutor(IConfiguration configuration) : base(configuration) { }

        protected override string ConnectionName => "Scheduled";
    }
}
