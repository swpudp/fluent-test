using FluentTest.Infrastructure.NpgSql;
using FluentTest.Scheduled.Model;
using FluentTest.Scheduled.Stories;
using Dapper.Extensions.Expression;


namespace FluentTest.Scheduled.NpgSql.Stores
{
    public class JobLogStore(IStoreExecutor storeExecutor) : IJobLogStore
    {
        private readonly IStoreExecutor _storeExecutor = storeExecutor;

        public Task<int> CreateAsync(JobLog entity)
        {
            return _storeExecutor.ExecuteAsync(c => c.InsertAsync(entity));
        }
    }
}
