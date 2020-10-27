using MonoRepo.Framework.Core.Constants;
using MonoRepo.Microservice.IdentityServer.B2B.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Helpers
{
    public class AccountHelper : IAccountHelper
    {
        private readonly IEmailService emailService;
        private readonly string productName = Products.Identity;

        public AccountHelper(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public Task SendConfirmationLink(string callbackUrl, string userId, string email)
        {
            return emailService.SendEmail(new EmailViewModel
            {
                Body = $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.",
                Subject = "Confirm your email",
                Recipients = email,
                Key = userId.ToString(),
                ProductName = productName,
            });
        }

        public Task SendResetPasswordLink(string callbackUrl, string userId, string email)
        {
            return emailService.SendEmail(new EmailViewModel
            {
                Body = $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.",
                Subject = "Reset your password",
                Recipients = email,
                Key = userId.ToString(),
                ProductName = productName,
            });
        }
    }
}
