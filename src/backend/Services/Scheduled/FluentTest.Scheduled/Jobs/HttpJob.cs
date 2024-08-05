using FluentTest.Scheduled.Stories;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Util;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FluentTest.Scheduled.Jobs;

public class HttpJob(IJobLogStore jobLogStore, IHttpClientFactory httpClientFactory, ILogger<HttpJob> logger) : AbstractJob(jobLogStore, logger)
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public override async Task DoExecute(IJobExecutionContext context)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();
        string method = context.MergedJobDataMap.GetString("method");
        if (method.IsNullOrWhiteSpace())
        {
            throw new Exception("未指定请求方式");
        }
        string url = context.MergedJobDataMap.GetString("url");
        if (url.IsNullOrWhiteSpace())
        {
            throw new Exception("未指定请求地址");
        }
        HttpResponseMessage httpResponse;
        if (string.Compare(method, "post", StringComparison.OrdinalIgnoreCase) == 0)
        {
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
            HttpContent content = JsonContent.Create(new { }, mediaType, _jsonSerializerOptions);
            httpResponse = await httpClient.PostAsync(url, content);
        }
        else
        {
            httpResponse = await httpClient.GetAsync(url);
        }
        context.Result = httpResponse.ToString();
    }
}
