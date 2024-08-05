using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FluentTest.Infrastructure.MsSql;

public abstract class AbstractMsSqlStoreExecutor
{
    private readonly IConfiguration _configuration;
    private readonly SqlConnection _connection;
    public AbstractMsSqlStoreExecutor(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = CreateConnection();
    }

    protected abstract string ConnectionName { get; }

    private SqlConnection CreateConnection()
    {
        string connectionString = _configuration.GetConnectionString(ConnectionName);
        SqlConnection connection = new SqlConnection(connectionString);
        return connection;
    }

    public Task ExecuteAsync(Func<IDbConnection, Task> action)
    {
        return action(_connection);
    }

    public Task<T> ExecuteAsync<T>(Func<IDbConnection, Task<T>> action)
    {
        return action(_connection);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _connection?.Dispose();
        }
    }
}
