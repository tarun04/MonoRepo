using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MonoRepo.Microservice.IdentityServer.B2C.Models
{
    public class RegisterViewModel
    {
        /// <summary>
        /// FirstName
        /// </summary>
        [Required]
        [StringLength(80)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        [Required]
        [StringLength(80)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [Remote("ValidatePasswordStrength", "Account", AdditionalFields = "TenantId")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords doesn't match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Id of the tenant
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Redirect url
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Login url
        /// </summary>
        public string LoginUrl { get; set; }
    }
}
