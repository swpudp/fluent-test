namespace FluentTest.Infrastructure;

/// <summary>
/// 带唯一键的实体接口
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IUniqueKeyEntity<TKey>
{
    TKey Id { get; }
}
