using Dapper.Extensions.Expression;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FluentTest.Identity.Stores
{
    public class RoleStore(IIdentityStoreExecutor storeExecutor, IdentityErrorDescriber describer) : RoleStoreBase<IdentityRole, string, IdentityUserRole, IdentityRoleClaim>(describer)
    {
        private readonly IIdentityStoreExecutor _storeExecutor = storeExecutor;
        private readonly IdentityErrorDescriber _describer = describer;

        public override IQueryable<IdentityRole> Roles => throw new NotImplementedException();

        public override Task AddClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken = default)
        {
            int result = await _storeExecutor.ExecuteAsync(conn => conn.InsertAsync(role));
            return result > 0 ? IdentityResult.Success : IdentityResult.Failed(_describer.DefaultError());
        }

        public override Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityRole> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Claim>> GetClaimsAsync(IdentityRole role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}