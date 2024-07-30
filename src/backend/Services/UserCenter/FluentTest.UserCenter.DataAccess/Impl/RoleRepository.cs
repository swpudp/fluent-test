using Dapper.Extensions.Expression;
using FluentTest.Infrastructure;
using FluentTest.UserCenter.DataAccess.Contract;
using FluentTest.UserCenter.Model.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace FluentTest.UserCenter.DataAccess.Impl
{
    public class RoleRepository : AbstractRepository, IRoleRepository
    {
        private readonly string _connectionString;

        public RoleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySql");
        }

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(Role role, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(Role role, string? roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override IDbConnection CreateConnection()
        {
            NpgsqlDataSourceBuilder builder = new NpgsqlDataSourceBuilder(_connectionString);
            NpgsqlDataSource dataSource = builder.Build();
            return dataSource.OpenConnection();
        }

        Task<Role?> IRoleStore<Role>.FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Role?> IRoleStore<Role>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Role IRoleRepository.GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}