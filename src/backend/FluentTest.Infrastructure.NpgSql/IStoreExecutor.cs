﻿using System.Data;

namespace FluentTest.Infrastructure.NpgSql
{
    public interface IStoreExecutor : IDisposable
    {
        Task ExecuteAsync(Func<IDbConnection, Task> action);

        Task<T> ExecuteAsync<T>(Func<IDbConnection, Task<T>> action);
    }
}