namespace MonoRepo.Microservice.IdentityServer.B2B.Models
{
    public class RoleClaimViewModel
    {
        /// <summary>
        /// Type of RoleClaim
        /// </summary>
        public string RoleClaimType { get; set; }

        /// <summary>
        /// Value of RoleClaim
        /// </summary>
        public string RoleClaimValue { get; set; }

        /// <summary>
        /// DisplayName of RoleClaim
        /// </summary>
        public string RoleClaimDisplayName { get; set; }
    }
}
