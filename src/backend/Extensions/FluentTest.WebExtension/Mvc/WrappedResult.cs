using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.WebExtension.Mvc;

public class WrappedResult
{
    public int Code { get; set; }

    public string? Message { get; set; }

    public WrappedResult()
    {
    }

    public WrappedResult(int code, string? message)
    {
        Code = code;
        Message = message;
    }
}


public class WrappedResult<TResponse> : WrappedResult
{
    public TResponse? Data { get; set; }

    public WrappedResult()
    {
    }

    public WrappedResult(int code, string? message, TResponse? data) : base(code, message)
    {
        Data = data;
    }
}
