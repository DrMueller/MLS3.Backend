using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Entities;
using Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Repositories;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.Native
{
    public sealed class AppUserStore : IUserPasswordStore<AppUser>
    {
        private readonly IAppUserRepository _appUserRepo;

        public AppUserStore(IAppUserRepository appUserRepo)
        {
            _appUserRepo = appUserRepo;
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            await _appUserRepo.SaveAsync(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            if (user.Id.HasValue)
            {
                await _appUserRepo.DeleteAsync(user.Id.Value);
            }

            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var id = long.Parse(userId);
            var user = await _appUserRepo.LoadByIdAsync(id);
            return user;
        }

        public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _appUserRepo.LoadByNormalizedUserNameAsync(normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.HashedPassword);
        }

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.HashedPassword));
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.HashedPassword = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            await _appUserRepo.SaveAsync(user);
            return IdentityResult.Success;
        }
    }
}