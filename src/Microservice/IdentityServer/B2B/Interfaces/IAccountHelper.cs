using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Interfaces
{
    /// <summary>
    /// Helper class that sends confirmation and reset password link.
    /// </summary>
    public interface IAccountHelper
    {
        /// <summary>
        /// Attempts to send confirmation link.
        /// </summary>
        /// <param name="callbackUrl"></param>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task SendConfirmationLink(string callbackUrl, string userId, string email);

        /// <summary>
        /// Attemps to send reset password link.
        /// </summary>
        /// <param name="callbackUrl"></param>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task SendResetPasswordLink(string callbackUrl, string userId, string email);
    }
}
