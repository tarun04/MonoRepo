using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities;
using MonoRepo.Microservice.IdentityServer.B2C.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2C.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2C.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MonoRepo.Microservice.IdentityServer.B2C.Command.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserViewModel>
    {
        private readonly IdentityB2CDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IAccountHelper accountHelper;
        private readonly ITenantService tenantService;

        public AddUserCommandHandler(
            IdentityB2CDbContext context,
            UserManager<User> userManager,
            IAccountHelper accountHelper,
            ITenantService tenantService)
        {
            this.context = context;
            this.userManager = userManager;
            this.accountHelper = accountHelper;
            this.tenantService = tenantService;
        }

        public async Task<UserViewModel> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // Check if the user already exists for this tenant.
            if (await context.AspNetUsers.AsNoTracking().AnyAsync(x =>
                x.Email == request.Email && x.TenantId == request.TenantId))
            {
                throw new InvalidOperationException("User already exists.");
            }

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                TenantId = request.TenantId,
                UserName = $"{request.Email}_{request.TenantId}",
                Email = request.Email
            };

            var result = await userManager.CreateAsync(newUser);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(",", result.Errors.Select(x => x.Description)));
            }
            var emailCode = await userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var passwordCode = await userManager.GeneratePasswordResetTokenAsync(newUser);

            var tenant = await tenantService.GetTenantById(request.TenantId.ToString());
            var productUrl = tenant.TenantProducts.FirstOrDefault().TenantProductUrl;

            var callbackUrl = string.Empty;
            if (productUrl.Contains("localhost"))
            {
                var baseUrl = "https://localhost:5002";
                callbackUrl = $"{baseUrl}/identity-b2b/Account/ConfirmAccount?userId={HttpUtility.UrlEncode(newUser.Id.ToString())}&code={HttpUtility.UrlEncode(emailCode)}";
            }
            else
            {
                if (productUrl.EndsWith("/"))
                    productUrl = productUrl.Remove(productUrl.Length - 1);

                var baseUrl = productUrl.Substring(0, productUrl.LastIndexOf("/"));
                callbackUrl = $"{baseUrl}/identity-b2c/Account/ConfirmAccount?userId={HttpUtility.UrlEncode(newUser.Id.ToString())}&code={HttpUtility.UrlEncode(emailCode)}";
            }


            if (request.SendConfirmationEmail)
                await accountHelper.SendConfirmationLink(callbackUrl, newUser.Id.ToString(), newUser.Email);

            return new UserViewModel
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                TenantId = newUser.TenantId,
                IsActive = newUser.IsActive,
                EmailCode = emailCode,
                PasswordCode = passwordCode
            };
        }
    }
}
