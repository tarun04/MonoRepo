using MonoRepo.Microservice.IdentityServer.B2C.Models;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2C.Interfaces
{
    /// <summary>
    /// Service class that encapsulates interaction with the Tenant Microservice.
    /// </summary>
    public interface ITenantService
    {
        /// <summary>
        /// Attempts to get a tenant from the tenant microservice by name.
        /// </summary>
        /// <param name="name">Name of the Tenant to get.</param>
        /// <returns><see cref="TenantViewModel"/> contains Tenant Information.</returns>
        Task<TenantViewModel> GetTenantByName(string name);

        /// <summary>
        /// Attempts to get a tenant from the tenant microservice by Id.
        /// </summary>
        /// <param name="id">Id of the Tenant to get.</param>
        /// <returns><see cref="TenantViewModel"/> contains Tenant Information.</returns>
        Task<TenantViewModel> GetTenantById(string id);
    }
}
