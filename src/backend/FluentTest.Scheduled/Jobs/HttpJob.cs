using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace FluentTest.Scheduled.Jobs
{
    public class HttpJob : AbstractJob
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpJob(IHttpClientFactory httpClientFactory, ILogger<HttpJob> logger) : base(logger)
        {
            _httpClientFactory = httpClientFactory;
        }

        public override async Task DoExecute(IJobExecutionContext context)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            object? methodTypeObj = context.Get("MethodType");
            if (methodTypeObj == null)
            {
                throw new Exception("未指定请求方式");
            }
            string? method = methodTypeObj.ToString();
            object? urlObj = context.Get("url");
            if (urlObj == null)
            {
                throw new Exception("未指定请求地址");
            }
            string? url = urlObj.ToString();
            HttpResponseMessage httpResponse = null;
            if (string.Compare(method, "post", StringComparison.OrdinalIgnoreCase) == 0)
            {
                MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                HttpContent content = JsonContent.Create(new { });

                httpResponse = await httpClient.PostAsync(url, content);
            }
            else
            {
                httpResponse = await httpClient.GetAsync(url);
            }
            string result = await httpResponse.Content.ReadAsStringAsync();
            context.Put("result", result);
        }
    }
}
