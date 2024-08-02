using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace FluentTest.Infrastructure.MySql;

public abstract class AbstractMySqlStoreExecutor
{
    private readonly IConfiguration _configuration;
    private readonly MySqlConnection _connection;
    public AbstractMySqlStoreExecutor(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = CreateConnection();
    }

    protected abstract string ConnectionName { get; }

    private MySqlConnection CreateConnection()
    {
        string connectionString = _configuration.GetConnectionString(ConnectionName);
        return new MySqlConnection(connectionString);
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
