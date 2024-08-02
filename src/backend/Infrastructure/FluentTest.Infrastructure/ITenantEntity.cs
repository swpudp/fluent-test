using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.Infrastructure;

/// <summary>
/// 租户实体接口
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface ITenantEntity<TKey>
{
    /// <summary>
    /// 租户Id
    /// </summary>
    TKey TenantId { get; }
}
