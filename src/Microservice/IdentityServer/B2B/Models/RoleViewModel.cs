using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Models
{
    public class RoleViewModel
    {
        /// <summary>
        /// Role Unique Identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of Role
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Description of Role
        /// </summary>
        public string RoleDescription { get; set; }

        /// <summary>
        /// Collection of Permissions for this Role
        /// </summary>
        public IEnumerable<RoleClaimViewModel> Permissions { get; set; }
    }
}
