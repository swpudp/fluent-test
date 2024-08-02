namespace FluentTest.WebExtension.Mvc
{
    /// <summary>
    /// 禁用自动格式化响应的声明
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableWrapperAttribute : Attribute
    {
    }
}
