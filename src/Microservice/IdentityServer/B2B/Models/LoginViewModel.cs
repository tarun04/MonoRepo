﻿using System.ComponentModel.DataAnnotations;

namespace MonoRepo.Microservice.IdentityServer.B2B.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// Login email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Login password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Flag representing if the login is to be remembered
        /// </summary>
        public bool RememberLogin { get; set; }

        /// <summary>
        /// Return url after login
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
