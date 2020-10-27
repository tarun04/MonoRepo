using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MonoRepo.Framework.Core.Extensions;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2B.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.ProfileServices
{
    /// <summary>
    /// Provides default claims that all ships 6 products need.  If a claim is needed by a product
    /// that isn't provided here it will need to be generated with a subsequent call.
    /// </summary>
    public class DefaultClaimsProfileService : IProfileService
    {
        private readonly UserManager<User> userManager;
        private readonly IdentityB2BDbContext context;
        private readonly IUserClaimsPrincipalFactory<User> claimsFactory;
        private readonly ITenantService tenantService;
        private readonly IMemoryCache memoryCache;
        private readonly ILogger logger;

        public DefaultClaimsProfileService(
            UserManager<User> userManager,
            IdentityB2BDbContext context,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            ITenantService tenantService,
            IMemoryCache memoryCache,
            ILogger<DefaultClaimsProfileService> logger)
        {
            this.userManager = userManager;
            this.context = context;
            this.claimsFactory = claimsFactory;
            this.tenantService = tenantService;
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        /// <summary>
        /// Builds default ships claims into security context for use in other gateways & microservices.
        /// </summary>
        /// <param name="context">Context used to get user information and set claims.</param>
        /// <exception cref="KeyNotFoundException">Throws if Tenant was not found in Tenant Microservice.</exception>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(sub);
            var principal = await claimsFactory.CreateAsync(user);
            var claims = principal.Claims.ToList();

            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            claims.Add(new Claim(JwtClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"));
            claims.Add(new Claim(Globals.ClaimsFirstName, user.FirstName));
            claims.Add(new Claim(Globals.ClaimsLastName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.Email, user.Email));

            var tenant = await GetTenant(user.TenantId.ToString());

            claims.Add(new Claim(Globals.ClaimsTenantId, tenant.Id.ToString()));
            claims.Add(new Claim(Globals.ClaimsTenantName, tenant.Name));
            claims.Add(new Claim(Globals.ClaimsTenantDisplayName, tenant.DisplayName));
            claims.Add(new Claim(Globals.ClaimsProducts, tenant.TenantProducts.ToJson()));

            var permissionsAndRoles = await GetPermissions(user);

            claims.AddRange(permissionsAndRoles.Permissions.Select(permission => new Claim(permission.Key, permission.Value)));

            claims.Add(new Claim(Globals.ClaimsRoleIds, permissionsAndRoles.Roles.ToJson()));
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }


        private async Task<TenantViewModel> GetTenant(string tenantId)
        {
            if (!memoryCache.TryGetValue(tenantId, out TenantViewModel tenant))
            {
                tenant = await tenantService.GetTenantById(tenantId);
                if (tenant == null) throw new KeyNotFoundException($"Could not find {nameof(tenant)} with {nameof(tenantId)}: {tenantId}");

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(10));

                memoryCache.Set(tenantId, tenant, cacheEntryOptions);
            }
            return tenant;
        }


        private async Task<RolesAndPermissionsViewModel> GetPermissions(User user)
        {
            var foundRoles = await context.UserRoles
                .AsNoTracking()
                .Where(x => x.UserId == user.Id)
                .Select(x => new { x.Role.Id, x.Role.Claims })
                .ToListAsync();

            return new RolesAndPermissionsViewModel
            {
                Permissions = foundRoles.SelectMany(x => x.Claims).Select(x => new KeyValuePair<string, string>(x.ClaimType, x.ClaimValue)).ToList(),
                Roles = foundRoles.Select(x => x.Id).ToList()
            };
        }
    }
}
