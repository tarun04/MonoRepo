using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using MonoRepo.Framework.Core.Extensions;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities;
using MonoRepo.Microservice.IdentityServer.B2C.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2C.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2C.ProfileServices
{
    /// <summary>
    /// Provides default claims that all monorepo products need.  If a claim is needed by a product
    /// that isn't provided here it will need to be generated with a subsequent call.
    /// </summary>
    public class DefaultClaimsProfileService : IProfileService
    {
        public const string ClaimsFirstName = "first_name";
        public const string ClaimsLastName = "last_name";

        private readonly UserManager<User> userManager;
        private readonly IdentityB2CDbContext context;
        private readonly IUserClaimsPrincipalFactory<User> claimsFactory;
        private readonly ITenantService tenantService;

        public DefaultClaimsProfileService(
            UserManager<User> userManager,
            IdentityB2CDbContext context,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            ITenantService tenantService)
        {
            this.userManager = userManager;
            this.context = context;
            this.claimsFactory = claimsFactory;
            this.tenantService = tenantService;
        }

        /// <summary>
        /// Builds default monorepo claims into security context for use in other gateways & microservices.
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
            claims.Add(new Claim(ClaimsFirstName, user.FirstName));
            claims.Add(new Claim(ClaimsLastName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.Email, user.Email));

            var tenant = await tenantService.GetTenantById(user.TenantId.ToString());
            if (tenant == null) throw new KeyNotFoundException($"Could not find {nameof(tenant)} with {nameof(user.TenantId)}: {user.TenantId}");

            claims.Add(new Claim(Globals.ClaimsTenantId, tenant.Id.ToString()));
            claims.Add(new Claim(Globals.ClaimsTenantName, tenant.Name));
            claims.Add(new Claim(Globals.ClaimsTenantDisplayName, tenant.DisplayName));
            claims.Add(new Claim(Globals.ClaimsProducts, tenant.TenantProducts.ToJson()));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
