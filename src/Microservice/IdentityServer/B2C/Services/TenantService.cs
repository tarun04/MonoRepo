using Microsoft.Extensions.Caching.Memory;
using MonoRepo.Framework.Core.Utility;
using MonoRepo.Microservice.IdentityServer.B2C.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2C.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2C.Services
{
    /// <inheritdoc />
    public class TenantService : ITenantService
    {
        private readonly HttpClient httpClient;
        private readonly MicroservicesHelper microservicesHelper;
        private readonly IMemoryCache memoryCache;

        public TenantService(IHttpClientFactory httpClientFactory, MicroservicesHelper microservicesHelper, IMemoryCache memoryCache)
        {
            this.httpClient = httpClientFactory.CreateClient("Tenant");
            this.microservicesHelper = microservicesHelper;
            this.memoryCache = memoryCache;
        }

        /// <inheritdoc />
        public async Task<TenantViewModel> GetTenantByName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (!memoryCache.TryGetValue(name, out TenantViewModel tenant))
            {
                var message = new HttpRequestMessage(HttpMethod.Get,
                    string.Format(microservicesHelper.GetPathByName("Tenant", nameof(GetTenantByName)), name));

                var response = await httpClient.SendAsync(message);

                response.EnsureSuccessStatusCode();
                tenant = JsonConvert.DeserializeObject<TenantViewModel>(await response.Content.ReadAsStringAsync());

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(10));

                memoryCache.Set(name, tenant, cacheEntryOptions);
            }
            return tenant;
        }

        /// <inheritdoc />
        public async Task<TenantViewModel> GetTenantById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            if (!memoryCache.TryGetValue(id, out TenantViewModel tenant))
            {
                var message = new HttpRequestMessage(HttpMethod.Get,
                string.Format(microservicesHelper.GetPathByName("Tenant", nameof(GetTenantById)), id));
                var response = await httpClient.SendAsync(message);

                response.EnsureSuccessStatusCode();

                tenant = JsonConvert.DeserializeObject<TenantViewModel>(await response.Content.ReadAsStringAsync());

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(10));

                memoryCache.Set(id, tenant, cacheEntryOptions);
            }
            return tenant;
        }
    }
}
