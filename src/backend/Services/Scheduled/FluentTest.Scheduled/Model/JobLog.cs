using Dapper.Extensions.Expression;
using FluentTest.Infrastructure;
using FluentTest.Scheduled.EnumCollection;

namespace FluentTest.Scheduled.Model;

[Table("qrtz_job_log"), TableNaming(NamingPolicy.SnakeCase), FieldNaming(NamingPolicy.SnakeCase)]
public class JobLog : IBaseEntity, IUniqueKeyEntity<string>, ITenantEntity<string>, ICreationEntity<string>
{
    public string Id { get; set; }

    public string TenantId { get; set; }

    public string JobName { get; set; }

    public string JobGroup { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public double Duration { get; set; }

    /// <summary>
    /// 执行状态
    /// </summary>
    public JobExecutionStatus JobStatus { get; set; }

    /// <summary>
    /// 失败原因
    /// </summary>
    public string FailReason { get; set; }

    public DateTime CreateTime { get; set; }

    public string CreatorId { get; set; }

    public string CreatorName { get; set; }
}
