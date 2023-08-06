using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FluentTest.WebExtension.Mvc
{
    public class ResultWrapperFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            context.Result = context.Result switch
            {
                ObjectResult result => new ObjectResult(new WrappedResult<object>(0, null, result.Value)),
                EmptyResult => new ObjectResult(new WrappedResult(0, null)),
                _ => context.Result
            };
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            //do nothing
        }
    }
}
