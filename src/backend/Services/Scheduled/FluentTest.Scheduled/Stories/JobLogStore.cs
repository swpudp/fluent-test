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

    public Task<IList<JobLog>> ListJobLogsAsync(string jobName, string jobGroup)
    {
        return _storeExecutor.ExecuteAsync(c => c.Query<JobLog>()
        .WhereIf(string.IsNullOrEmpty(jobName), f => f.JobName.Equals(jobName))
        .WhereIf(string.IsNullOrEmpty(jobGroup), f => f.JobGroup.Equals(jobGroup))
        .OrderByDescending(x => x.Id)
        .ToListAsync<JobLog>());
    }
}
