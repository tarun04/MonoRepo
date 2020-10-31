using System;

namespace MonoRepo.Microservice.IdentityServer.B2C.Models
{
    public class UserViewModel
    {
        /// <summary>
        /// Id of the User
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// FirstName of the User
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName of the User
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Id of the Tenant
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// Flag representing if this User is Active or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Email of the User
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// PhoneNumber of the User
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email token
        /// </summary>
        public string EmailCode { get; set; }

        /// <summary>
        /// Password token
        /// </summary>
        public string PasswordCode { get; set; }
    }
}
