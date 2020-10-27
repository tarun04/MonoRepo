using MonoRepo.Microservice.IdentityServer.B2C.Models;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2C.Interfaces
{
    /// <summary>
    /// Service class that encapsulates interaction with the Email service.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Attempts to send an email.
        /// </summary>
        /// <param name="email">Email to be sent.</param>
        /// <returns></returns>
        Task SendEmail(EmailViewModel email);
    }
}
