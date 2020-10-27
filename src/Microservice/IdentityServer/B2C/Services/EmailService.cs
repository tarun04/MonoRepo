using MonoRepo.Framework.Core.Extensions;
using MonoRepo.Framework.Core.Utility;
using MonoRepo.Microservice.IdentityServer.B2C.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2C.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2C.Services
{
    /// <inheritdoc />
    public class EmailService : IEmailService
    {
        private const string emailClientName = "Email";
        private readonly HttpClient httpClient;
        private readonly MicroservicesHelper microservicesHelper;

        public EmailService(IHttpClientFactory httpClientFactory, MicroservicesHelper microservicesHelper)
        {
            this.httpClient = httpClientFactory.CreateClient(emailClientName);
            this.microservicesHelper = microservicesHelper;
        }

        /// <inheritdoc />
        public async Task SendEmail(EmailViewModel email)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));

            var message = new HttpRequestMessage(HttpMethod.Post, string.Format(microservicesHelper.GetPathByName("Email", "SendEmail")))
            {
                Content = new StringContent(email.ToJson(), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
        }
    }
}
