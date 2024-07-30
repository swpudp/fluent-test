using System.Data;

namespace FluentTest.Infrastructure
{
    public abstract class AbstractRepository
    {
        protected abstract IDbConnection CreateConnection();

        protected async Task Execute(Func<IDbConnection, Task> action)
        {
            using (IDbConnection connection = CreateConnection())
            {
                await action(connection);
            }
        }

        protected async Task<T> Execute<T>(Func<IDbConnection, Task<T>> action)
        {
            using (IDbConnection connection = CreateConnection())
            {
                return await action(connection);
            }
        }

        protected int ExecuteTransaction(Func<IDbConnection, IDbTransaction, IEnumerable<int>> actions)
        {
            using (IDbConnection connection = CreateConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    IEnumerable<int> total = actions(connection, transaction);
                    transaction.Commit();
                    return total.Sum();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}