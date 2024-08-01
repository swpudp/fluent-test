using Dapper;
using Dapper.Extensions.Expression;
using FluentTest.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FluentTest.Identity.Stores
{
    public class UserStore(IIdentityStoreExecutor storeExecutor, IdentityErrorDescriber describer) : UserStoreBase<IdentityUser, string, IdentityUserClaim, IdentityUserLogin, IdentityUserToken>(describer)
    {
        private readonly IIdentityStoreExecutor _storeExecutor = storeExecutor;
        private readonly IdentityErrorDescriber _describer = describer;

        #region 用户操作
        public override async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            user.Id = ObjectId.GenerateNewId().ToString();
            int result = await _storeExecutor.ExecuteAsync(conn => conn.InsertAsync(user));
            return result > 0 ? IdentityResult.Success : IdentityResult.Failed(_describer.DefaultError());
        }

        public override async Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            int result = await _storeExecutor.ExecuteAsync(conn => conn.DeleteAsync(user));
            return result > 0 ? IdentityResult.Success : IdentityResult.Failed(_describer.DefaultError());
        }

        public override Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return _storeExecutor.ExecuteAsync(conn => conn.Query<IdentityUser>().Where(x => x.NormalizedEmail == normalizedEmail).FirstOrDefaultAsync<IdentityUser>());
        }

        public override Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return _storeExecutor.ExecuteAsync(conn => conn.Query<IdentityUser>().Where(x => x.Id == userId).FirstOrDefaultAsync<IdentityUser>());
        }

        public override Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return _storeExecutor.ExecuteAsync(conn => conn.Query<IdentityUser>().Where(x => x.NormalizedUserName == normalizedUserName).FirstOrDefaultAsync<IdentityUser>());
        }

        public override async Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            int result = await _storeExecutor.ExecuteAsync(conn => conn.UpdateAsync(user));
            return result > 0 ? IdentityResult.Success : IdentityResult.Failed(_describer.DefaultError());
        }

        protected override Task<IdentityUser> FindUserAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return _storeExecutor.ExecuteAsync(conn => conn.Query<IdentityUser>().Where(x => x.Id == userId).FirstOrDefaultAsync<IdentityUser>());
        }

        public override IQueryable<IdentityUser> Users => throw new NotImplementedException();

        public override Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken = default)
        {
            return base.SetPasswordHashAsync(user, passwordHash, cancellationToken);
        }

        #endregion

        public override Task AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task AddLoginAsync(IdentityUser user, UserLoginInfo login, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }



        public override Task<IList<Claim>> GetClaimsAsync(IdentityUser user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override async Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await _storeExecutor.ExecuteAsync(conn => conn.Query<IdentityUserLogin>().Where(l => l.UserId.Equals(user.Id)).Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)).ToListAsync<UserLoginInfo>());
        }

        public override Task<IList<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveLoginAsync(IdentityUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task ReplaceClaimAsync(IdentityUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected override Task AddUserTokenAsync(IdentityUserToken token)
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserToken> FindTokenAsync(IdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }



        protected override Task<IdentityUserLogin> FindUserLoginAsync(string userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task RemoveUserTokenAsync(IdentityUserToken token)
        {
            throw new NotImplementedException();
        }
    }
}