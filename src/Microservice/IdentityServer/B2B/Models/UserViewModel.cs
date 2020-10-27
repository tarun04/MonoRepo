using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Models
{
    public class UserViewModel
    {
        /// <summary>
        /// Id of the User
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// FirstName of the User
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName of the User
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Flag representing if this User is Enabled or not
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Email of the User
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Username of the User
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Collection of Roles for this User
        /// </summary>
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}
