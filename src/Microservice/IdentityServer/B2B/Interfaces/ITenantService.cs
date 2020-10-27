using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Interfaces
{
    /// <summary>
    /// Service class that encapsulates interaction with the Tenant Microservice.
    /// </summary>
    public interface ITenantService
    {
        /// <summary>
        /// Attempts to get a tenant from the tenant microservice by Id.
        /// </summary>
        /// <param name="id">Id of the Tenant to get.</param>
        /// <returns><see cref="TenantViewModel"/> contains Tenant Information.</returns>
        Task<TenantViewModel> GetTenantById(string id);
    }
}
