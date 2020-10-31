using MediatR;
using Microsoft.AspNetCore.Identity;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using MonoRepo.Microservice.IdentityServer.B2B.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid>
    {
        private readonly Framework.Core.Security.IdentityUser identityUser;
        private readonly UserManager<User> userManager;
        private readonly IAccountHelper accountHelper;
        private readonly ITenantService tenantService;

        public AddUserCommandHandler(
            Framework.Core.Security.IdentityUser identityUser,
            UserManager<User> userManager,
            IAccountHelper accountHelper,
            ITenantService tenantService)
        {
            this.identityUser = identityUser;
            this.userManager = userManager;
            this.accountHelper = accountHelper;
            this.tenantService = tenantService;
        }

        public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsEnabled = request.IsEnabled,
                TenantId = identityUser.TenantId,
                UserName = $"{request.Email}_{identityUser.TenantId}",
                Email = request.Email
            };

            var result = await userManager.CreateAsync(newUser);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(",", result.Errors.Select(x => x.Description)));
            }

            var roleAssignment = await userManager.AddToRolesAsync(newUser, request.Roles.Select(x => $"{x.RoleName.Replace(" ", "")}_{identityUser.TenantId}"));

            var emailCode = await userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var tenant = await tenantService.GetTenantById(identityUser.TenantId.ToString());
            var productUrl = tenant.TenantProducts.FirstOrDefault().TenantProductUrl;

            var callbackUrl = string.Empty;
            if (productUrl.Contains("localhost"))
            {
                var baseUrl = "https://localhost:5001";
                callbackUrl = $"{baseUrl}/identity-b2b/Account/ConfirmAccount?userId={HttpUtility.UrlEncode(newUser.Id.ToString())}&code={HttpUtility.UrlEncode(emailCode)}";
            }
            else
            {
                if (productUrl.EndsWith("/"))
                    productUrl = productUrl.Remove(productUrl.Length - 1);

                var baseUrl = productUrl.Substring(0, productUrl.LastIndexOf("/"));
                callbackUrl = $"{baseUrl}/identity-b2b/Account/ConfirmAccount?userId={HttpUtility.UrlEncode(newUser.Id.ToString())}&code={HttpUtility.UrlEncode(emailCode)}";
            }

            await accountHelper.SendConfirmationLink(callbackUrl, newUser.Id.ToString(), newUser.Email);

            return newUser.Id;
        }
    }
}
