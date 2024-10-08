﻿using FluentTest.Infrastructure;
using FluentTest.Scheduled.Stories;
using FluentTest.Scheduled.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FluentTest.Scheduled.Jobs;

public class MediatJob(IMediator mediator, IJobLogStore jobLogStore, ILogger<HttpJob> logger) : AbstractJob(jobLogStore, logger)
{
    private readonly IMediator _mediator = mediator;

    public override async Task DoExecute(IJobExecutionContext context)
    {
        string typeName = context.MergedJobDataMap.GetString("type");
        if (string.IsNullOrEmpty(typeName))
        {
            throw new BusinessExpcetion("未找到类型名称");
        }
        JsonObject json = context.MergedJobDataMap.BuildJsonObject("type");
        Type requestType = TypeLoaderUtil.LoadRequestHandler(typeName);
        object requestObj = json.Deserialize(requestType, ConstUtil.JsonSerializerOptions);
        if (requestObj is not IRequest request)
        {
            throw new BusinessExpcetion("未找到类型名称");
        }
        await _mediator.Send(request);
    }
}
