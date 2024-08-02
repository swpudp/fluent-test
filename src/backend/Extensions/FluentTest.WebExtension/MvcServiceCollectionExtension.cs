using FluentTest.WebExtension.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace FluentTest.WebExtension
{
    public static class MvcServiceCollectionExtension
    {
        public static IMvcBuilder AddApiControllers(this IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddControllers();
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ResponseWrapApplicationModelProvider>());
            mvcBuilder.ConfigureApiBehaviorOptionsEx();
            return mvcBuilder;
        }

        public static IMvcBuilder ConfigureApiBehaviorOptionsEx(this IMvcBuilder builder)
        {
            builder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    List<Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry> errors = context.ModelState.Values.ToList();
                    string msg = string.Join(";", errors.Select(item => string.Join(";", item.Errors.Select(x => x.ErrorMessage).ToList())).ToList());
                    ObjectResult result = new(new WrappedResult(StatusCodes.Status400BadRequest, msg));
                    //add `using System.Net.Mime;` to resolve MediaTypeNames
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    result.ContentTypes.Add(MediaTypeNames.Application.Xml);
                    return result;
                };
            });
            return builder;
        }
    }
}
