using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Net;

namespace FluentTest.WebExtension.Mvc
{
    /// <summary>
    /// 统一格式化响应结果
    /// </summary>
    public class ResponseWrapApplicationModelProvider : IApplicationModelProvider
    {
        public int Order => 0;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {

        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (ControllerModel controller in context.Result.Controllers)
            {
                if (controller.Attributes.OfType<DisableWrapperAttribute>().Any())
                {
                    continue;
                }
                foreach (ActionModel action in controller.Actions)
                {
                    if (action.Attributes.OfType<DisableWrapperAttribute>().Any())
                    {
                        continue;
                    }
                    AddFilters(action);
                }
            }
        }

        private static void AddFilters(ActionModel action)
        {
            if (!action.Filters.Any(e => (e is ProducesResponseTypeAttribute producesResponseType) && producesResponseType.StatusCode == StatusCodes.Status200OK))
            {
                Type? returnType = null;
                if (action.ActionMethod.ReturnType.GenericTypeArguments.Any())
                {
                    returnType = action.ActionMethod.ReturnType.GetGenericArguments()[0];
                }
                Type type = returnType is null ? typeof(WrappedResult) : typeof(WrappedResult<>).MakeGenericType(returnType);
                action.Filters.Add(new ProducesResponseTypeAttribute(type, StatusCodes.Status200OK));
                action.Filters.Add(new ResultWrapperFilter());
            }
        }
    }
}
