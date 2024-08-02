using Dapper.Extensions.Expression;
using FluentTest.Scheduled.Model;


namespace FluentTest.Scheduled.Stories;

public class JobLogStore(IScheduledStoreExecutor storeExecutor) : IJobLogStore
{
    private readonly IScheduledStoreExecutor _storeExecutor = storeExecutor;

    public Task<int> CreateAsync(JobLog entity)
    {
        return _storeExecutor.ExecuteAsync(c => c.InsertAsync(entity));
    }
}
