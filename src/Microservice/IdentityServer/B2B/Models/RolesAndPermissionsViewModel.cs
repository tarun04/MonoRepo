using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Models
{
    public class RolesAndPermissionsViewModel
    {
        /// <summary>
        /// Collection of Permissions
        /// </summary>
        public IReadOnlyCollection<KeyValuePair<string, string>> Permissions { get; set; }

        /// <summary>
        /// Collection of Roles
        /// </summary>
        public IReadOnlyList<Guid> Roles { get; set; }
    }
}
