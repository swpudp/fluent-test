﻿using FluentTest.Scheduled.Model;

namespace FluentTest.Scheduled.Stories;

public interface IJobLogStore
{
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="entity">日志实体</param>
    /// <returns>写入数量</returns>
    Task<int> CreateAsync(JobLog entity);

    /// <summary>
    /// 日志列表
    /// </summary>
    /// <returns></returns>
    Task<IList<JobLog>> ListJobLogsAsync(string jobName,string jobGroup);
}
