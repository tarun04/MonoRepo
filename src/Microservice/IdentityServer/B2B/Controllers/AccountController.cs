using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MonoRepo.Framework.Extensions.Attributes;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using MonoRepo.Microservice.IdentityServer.B2B.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IAccountHelper accountHelper;
        private readonly ITenantService tenantService;
        private readonly IIdentityServerInteractionService interactionService;
        private readonly ILogger logger;

        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IAccountHelper accountHelper,
            ITenantService tenantService,
            IIdentityServerInteractionService interactionService,
            ILogger<AccountController> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.accountHelper = accountHelper;
            this.tenantService = tenantService;
            this.interactionService = interactionService;
            this.logger = logger;
        }

        /// <summary>
        /// Builds a LoginViewModel and returns the login View.
        /// </summary>
        /// <param name="returnUrl">Return URL provided by Identity Server.</param>
        /// <returns>The login view for user to enter email and passowrd.</returns>
        [HttpGet("[action]")]
        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
            });
        }

        /// <summary>
        /// Attempts to login the user by email and password.
        /// </summary>
        /// <param name="loginViewModel">Viewmodel containing username, password, and redirect url.</param>
        /// <returns>Error if login failed or redirect if successful.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(loginViewModel.Email);

                if (string.IsNullOrEmpty(loginViewModel.Email) || string.IsNullOrEmpty(loginViewModel.Password))
                {
                    return View(loginViewModel);
                }

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The Email address or Password is incorrect.");
                    loginViewModel.Password = string.Empty;
                    logger.LogDebug($"User not found: {loginViewModel.Email}");
                    return View(loginViewModel);
                }

                var result = await signInManager.PasswordSignInAsync(user,
                                                                     loginViewModel.Password,
                                                                     loginViewModel.RememberLogin,
                                                                     false);
                if (result.Succeeded)
                {
                    logger.LogDebug($"Login Successful redirecting user: {loginViewModel.Email}");
                    return Redirect(loginViewModel.ReturnUrl);
                }

                logger.LogDebug($"Login Failed for user :{loginViewModel.Email}");
                ModelState.AddModelError(string.Empty, "The Email address or Password is incorrect.");
                loginViewModel.Password = string.Empty;
                return View(loginViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DEBUG", ex.ToString());
                logger.LogCritical(ex, $"Error logging in for user: {loginViewModel.Email}");
                return View(loginViewModel);
            }
        }

        /// <summary>
        /// Attempts to send email confirmation link.
        /// </summary>
        /// <param name="userId">User Id of account to send the confirmation link for.</param>
        /// <returns></returns>
        [HttpGet("sendconfirmationemail/{userId}")]
        [IdentityUserFilter]
        public async Task<IActionResult> SendConfirmationLink([FromRoute] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var emailCode = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("confirmaccount", "Account", values: new { userId = userId, code = emailCode }, Request.Scheme);
            var email = user.Email;

            await accountHelper.SendConfirmationLink(callbackUrl, userId, email);
            return Ok();
        }

        /// <summary>
        /// Attempts to logout the user.
        /// </summary>
        /// <param name="logoutId">Logout Id of the user.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> LogOut(string logoutId)
        {
            var context = await interactionService.GetLogoutContextAsync(logoutId);
            if (context?.ClientId != null)
            {
                // if the logout request is authenticated, it's safe to automatically sign-out
                await signInManager.SignOutAsync();
                var logout = await interactionService.GetLogoutContextAsync(logoutId);
                return Redirect(logout?.PostLogoutRedirectUri);
            }
            return BadRequest("This user isn't authenticated.");
        }

        /// <summary>
        /// Builds a ResetPasswordViewModel and returns the forgot password View.
        /// </summary>
        /// <returns>The forgot password view for user to enter email.</returns>
        [HttpGet("[action]")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Attempts to send reset password link.
        /// </summary>
        /// <param name="model">Viewmodel containing email.</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
                string pwdCode = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("SetPassword", "Account", values: new { userId = user.Id, code = pwdCode }, Request.Scheme);

                await accountHelper.SendResetPasswordLink(callbackUrl, user.Id.ToString(), user.Email);

                return View("ForgotPasswordConfirmation", "Account");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Attempts to confirm a user account.
        /// </summary>
        /// <param name="userId">User Id of the account to be confirmed.</param>
        /// <param name="code">Confirmation code to confirm account.</param>
        /// <returns>The account confirmed view.</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult> ConfirmAccount(string userId, string code)
        {
            if (userId == default || code == default)
            {
                return View("AccountConfirmed");
            }

            var user = await userManager.FindByIdAsync(userId);
            var pwdRecoveryCode = await userManager.GeneratePasswordResetTokenAsync(user);

            if (await userManager.IsEmailConfirmedAsync(user))
            {
                if (await userManager.HasPasswordAsync(user))
                    return View("AccountConfirmed");
                else
                    return RedirectToAction("SetPassword", new SetPasswordViewModel() { Email = user.Email, Code = pwdRecoveryCode });
            }

            var result = await userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                return View("AccountConfirmed");
            }
            return RedirectToAction("SetPassword", new SetPasswordViewModel() { Email = user.Email, Code = pwdRecoveryCode });
        }

        /// <summary>
        /// Builds a SetPasswordViewModel and returns the set password View.
        /// </summary>
        /// <param name="email">Email of the user.</param>
        /// <param name="code">Code for setting password.</param>
        /// <returns>The set password view for user to enter email, password, confirm password and code.</returns>
        [HttpGet("[action]")]
        public IActionResult SetPassword(string email, string code)
        {
            return View(new SetPasswordViewModel { Email = email, Code = code });
        }

        /// <summary>
        /// Attempts to set password of the user.
        /// </summary>
        /// <param name="model">Viewmodel containing email, password, confirm password and code.</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            var tenantViewModel = new TenantViewModel();
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false)))
                {
                    return View("SetPasswordConfirmation");
                }

                await userManager.RemovePasswordAsync(user).ConfigureAwait(false);

                // TODO: [ap] show the errors when setting a password
                if (!await userManager.HasPasswordAsync(user).ConfigureAwait(false))
                {
                    var result = await userManager.AddPasswordAsync(user, model.Password).ConfigureAwait(false);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }
                else
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password).ConfigureAwait(false);
                }
                tenantViewModel = await tenantService.GetTenantById(user.TenantId.ToString()).ConfigureAwait(false);
            }
            else return View(model);

            return View("SetPasswordConfirmation", tenantViewModel);
        }

        /// <summary>
        /// Attempts to reset password of a user.
        /// </summary>
        /// <param name="userId">User Id of account to reset password for.</param>
        /// <returns></returns>
        [HttpGet("resetpassword/{userId}")]
        [AllowAnonymous]
        [IdentityUserFilter]
        public async Task<ActionResult> ResetPassword([FromRoute] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var pwdRecoveryCode = await userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("SetPassword", "Account", values: new { Email = user.Email, code = pwdRecoveryCode }, Request.Scheme);
            var email = user.Email;

            await accountHelper.SendResetPasswordLink(callbackUrl, userId, email);
            return Ok();
        }
    }
}
