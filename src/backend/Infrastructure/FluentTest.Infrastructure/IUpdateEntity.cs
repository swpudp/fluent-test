namespace FluentTest.Infrastructure;

/// <summary>
/// 带更新信息的实体
/// </summary>
public interface IUpdateEntity<TKey>
{
    TKey UpdateId { get; }

    string UpdatedName { get; }

    DateTime? UpdatedTime { get; }
}
