using MonoRepo.Framework.Core.Extensions;
using MonoRepo.Framework.Core.Utility;
using MonoRepo.Microservice.IdentityServer.B2B.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Services
{
    /// <inheritdoc />
    public class TenantService : ITenantService
    {
        private const string TenantClientName = "Tenant";
        private readonly HttpClient httpClient;
        private readonly MicroservicesHelper microservicesHelper;

        public TenantService(IHttpClientFactory httpClientFactory, MicroservicesHelper microservicesHelper)
        {
            this.httpClient = httpClientFactory.CreateClient(TenantClientName);
            this.microservicesHelper = microservicesHelper;
        }

        /// <inheritdoc />
        public async Task<TenantViewModel> GetTenantById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            var message = new HttpRequestMessage(HttpMethod.Get,
                string.Format(microservicesHelper.GetPathByName("Tenant", "GetTenantById"), id));
            var response = await httpClient.SendAsync(message);

            response.EnsureSuccessStatusCode();

            var tenant = (await response.Content.ReadAsStringAsync()).FromJson<TenantViewModel>();
            return tenant;
        }
    }
}
