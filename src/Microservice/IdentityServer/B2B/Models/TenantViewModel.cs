using MonoRepo.Framework.Core.Security;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Models
{
    public class TenantViewModel
    {
        /// <summary>
        /// Id of the Tenant
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the Tenant
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DisplayName of the Tenant
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Collection of Products for this Tenant
        /// </summary>
        public IReadOnlyList<UserProduct> TenantProducts { get; set; }
    }
}
