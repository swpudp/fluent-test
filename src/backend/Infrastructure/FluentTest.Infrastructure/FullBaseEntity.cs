namespace FluentTest.Infrastructure;

/// <summary>
/// 带完整信息的实体
/// </summary>
/// <typeparam name="TKey">实体键类型</typeparam>
public class FullBaseEntity<TKey> : IBaseEntity
    , ITenantEntity<TKey>
    , IUniqueKeyEntity<TKey>
    , ICreationEntity<TKey>
    , IUpdateEntity<TKey>
{
    public TKey TenantId { get; set; }

    public TKey Id { get; set; }

    public DateTime CreateTime { get; set; }

    public TKey CreatorId { get; set; }

    public string CreatorName { get; set; }

    public TKey UpdateId { get; set; }

    public string UpdatedName { get; set; }

    public DateTime? UpdatedTime { get; set; }
}
