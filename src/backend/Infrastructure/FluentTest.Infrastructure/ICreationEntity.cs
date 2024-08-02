namespace FluentTest.Infrastructure;

/// <summary>
/// 带创建信息的实体接口
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface ICreationEntity<TKey>
{
    DateTime CreateTime { get; }

    TKey CreatorId { get; }

    string CreatorName { get; }
}
