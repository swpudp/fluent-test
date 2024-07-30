using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace FluentTest.Infrastructure.NpgSql
{
    public abstract class AbstractNpgSqlStoreExecutor : IStoreExecutor
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection _connection;
        public AbstractNpgSqlStoreExecutor(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = CreateConnection();
        }

        protected abstract string ConnectionName { get; }

        private NpgsqlConnection CreateConnection()
        {
            string connectionString = _configuration.GetConnectionString(ConnectionName);
            NpgsqlDataSourceBuilder builder = new(connectionString);
            NpgsqlDataSource dataSource = builder.Build();
            return dataSource.OpenConnection();
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
}
