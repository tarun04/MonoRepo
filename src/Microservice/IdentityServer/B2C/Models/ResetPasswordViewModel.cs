using System.ComponentModel.DataAnnotations;

namespace MonoRepo.Microservice.IdentityServer.B2C.Models
{
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
