using FluentTest.Scheduled.Stories;
using FluentTest.Scheduled.Utils;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Util;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace FluentTest.Scheduled.Jobs;

public class HttpJob(IJobLogStore jobLogStore, IHttpClientFactory httpClientFactory, ILogger<HttpJob> logger) : AbstractJob(jobLogStore, logger)
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public override async Task DoExecute(IJobExecutionContext context)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();
        string method = context.MergedJobDataMap.GetString(ConstUtil.MethodKey);
        if (method.IsNullOrWhiteSpace())
        {
            throw new Exception("未指定请求方式");
        }
        string url = context.MergedJobDataMap.GetString(ConstUtil.UrlKey);
        if (url.IsNullOrWhiteSpace())
        {
            throw new Exception("未指定请求地址");
        }
        HttpResponseMessage httpResponse;
        if (string.Compare(method, ConstUtil.MethodPostKey, StringComparison.OrdinalIgnoreCase) == 0)
        {
            JsonObject json = context.MergedJobDataMap.BuildJsonObject(ConstUtil.MethodKey, ConstUtil.UrlKey);
            HttpContent content = JsonContent.Create(json, ConstUtil.JsonMediaType, ConstUtil.JsonSerializerOptions);
            httpResponse = await httpClient.PostAsync(url, content);
        }
        else
        {
            httpResponse = await httpClient.GetAsync(url);
        }
        context.Result = httpResponse.ToString();
    }
}
