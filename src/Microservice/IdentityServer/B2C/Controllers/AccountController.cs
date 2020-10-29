using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MonoRepo.Framework.Extensions.Attributes;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Constants;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Enums;
using MonoRepo.Microservice.IdentityServer.B2C.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2C.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2C.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2C.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IdentityB2CDbContext identityB2CDbContext;
        private readonly IAccountHelper accountHelper;
        private readonly ITenantService tenantService;
        private readonly IIdentityServerInteractionService interactionService;
        private readonly ILogger logger;

        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IdentityB2CDbContext identityB2CDbContext,
            IAccountHelper accountHelper,
            ITenantService tenantService,
            IIdentityServerInteractionService interactionService,
            ILogger<AccountController> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.identityB2CDbContext = identityB2CDbContext;
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
        public async Task<IActionResult> Login([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            var context = await interactionService.GetAuthorizationContextAsync(returnUrl);
            var tenantName = context?.Parameters[Constants.TenantName];
            var loginUrl = string.Concat(Request.Scheme, "://", Request.Host.ToUriComponent(),
                Request.PathBase.ToUriComponent(), Request.Path.ToUriComponent(), Request.QueryString.ToUriComponent());

            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
                TenantName = tenantName,
                LoginUrl = loginUrl
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
            // Try to get the tenant name fromthe return url.
            var context = await interactionService.GetAuthorizationContextAsync(loginViewModel.ReturnUrl);
            var tenantName = context?.Parameters[Constants.TenantName];
            // Check if tenant name is null, if so return error.
            if (string.IsNullOrEmpty(tenantName))
                return View("Error");

            // Check for a possible port number.
            if (tenantName.Contains(':'))
                tenantName = tenantName.Split(':')[0];

            // Retrieve tenant and check if it was found, if not return error view.
            var tenant = await tenantService.GetTenantByName(tenantName);
            if (tenant == null || string.IsNullOrEmpty(tenant.Id))
                return View("Error");

            // Retrieve user by email.
            var matchedUser = await identityB2CDbContext.AspNetUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == loginViewModel.Email && x.TenantId == new Guid(tenant.Id))
                .ConfigureAwait(false);

            // Check if user has entered email
            if (string.IsNullOrEmpty(loginViewModel.Email) || string.IsNullOrEmpty(loginViewModel.Password))
            {
                return View(loginViewModel);
            }
            // Check that a user was found.
            else if (matchedUser == null)
            {
                ModelState.AddModelError(string.Empty, "The username or password is incorrect.");
                loginViewModel.Password = string.Empty;
                return View(loginViewModel);
            }

            var result = await signInManager.PasswordSignInAsync(matchedUser,
                loginViewModel.Password,
                loginViewModel.RememberLogin,
                false).ConfigureAwait(false);


            if (result.Succeeded) return Redirect(loginViewModel.ReturnUrl);

            ModelState.AddModelError(string.Empty, "The username or password is incorrect.");
            loginViewModel.Password = string.Empty;
            return View(loginViewModel);
        }

        /// <summary>
        /// User is brought to Register form and we are determing which tenant he or she is.
        /// </summary>
        /// <param name="redirectUrl">The url to which the user is redirected after successful registration</param>
        /// <param name="tenantName">The name of tenant name i.e. company on the basis of which we are determing tenant Id</param>
        /// <param name="loginUrl">Url to redirect too in order to log in.  This will redirect back to the consuming app in order to generate the needed items client side and then redirect back to the login page.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Register([FromQuery] string redirectUrl, [FromQuery] string tenantName, [FromQuery] string loginUrl)
        {
            if (string.IsNullOrEmpty(tenantName) || string.IsNullOrEmpty(redirectUrl))
                return View("Error");

            if (tenantName.Contains(':'))
                tenantName = tenantName.Split(':')[0];

            // go get tenant data by tenant name
            var tenant = await tenantService.GetTenantByName(tenantName);
            if (tenant == null || string.IsNullOrEmpty(tenant.Id))
            {
                logger.LogError($"Unable to find tenant for name: {tenantName}");
                return View("Error");
            }

            return View(new RegisterViewModel
            {
                RedirectUrl = redirectUrl,
                TenantId = tenant?.Id,
                LoginUrl = loginUrl
            });
        }

        /// <summary>
        /// Registering a new user and sending the confirmation account email back to him.
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            if (string.IsNullOrEmpty(model.TenantId) ||
                string.IsNullOrEmpty(model.RedirectUrl))
                return View("Error");

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = $"{model.TenantId}_{model.Email}",
                TenantId = new Guid(model.TenantId)
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Errors.Any(x => x.Code == "DuplicateUserName"))
            {
                ModelState.AddModelError(string.Empty, "Email is already taken.");
                return View(model);
            }

            if (result.Succeeded)
            {
                //if a new user is created, generate purpose token for email confirmation action
                string code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = Url.Action("EmailConfirmation", "Account", values: new { email = user.Email, code = code, redirectUrl = model.RedirectUrl }, Request.Scheme);
                //send the confirmation email to the user
                await accountHelper.SendConfirmationLink(callbackUrl, user.Id.ToString(), user.Email);

                //return and login the user
                await signInManager.SignInAsync(user, isPersistent: false);
                return Redirect(model.RedirectUrl);
            }
            else
            {
                //if user is not created, show the possible errors 
                ModelState.AddModelError(string.Empty, "Register Failed.");
                return View(model);
            }
        }

        /// <summary>
        /// Confirms the new user email, and with that the finishing the registration process 
        /// </summary>
        /// <param name="email">Email that needs to be confirmed</param>
        /// <param name="code">Purpose token</param>
        /// <param name="redirectUrl">Url to where user needs to be redirected after successful email confirmation</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> EmailConfirmation(string email, string code, string redirectUrl)
        {
            //1) Try to get a user with his email
            var user = await userManager.FindByEmailAsync(email).ConfigureAwait(false);

            //2) If a user doesn't exist, do not notify him, just return the success registration view
            if (user == null)
                return View("SuccessRegistration");

            //3) Validate the purpose token
            if (!await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider,
                "EmailConfirmation", code))
            {
                return View("SuccessRegistration");
            }

            //4) if we have a valid token, then confirm the email
            await userManager.ConfirmEmailAsync(user, code);
            //5) show the user email confirmation success view
            return View("EmailConfirmation", redirectUrl);
        }


        /// <summary>
        /// Creates a new user via an API call instead of a html form filled in by the user.
        /// This is used for creating users from business logic in other microservices.
        /// </summary>
        /// <param name="model"><see cref="RegisterViewModel"/></param>
        /// <returns>The Id of the new User.</returns>
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public async Task<IActionResult> ApiRegister([FromBody] RegisterViewModel model)
        {
            /**
             * BIG TODO [at]:
             * We need to secure this end point somehow.
             */
            // Check if the model state is valid, if not return bad request.
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            // Check if the tenant id provided is valid.  If not return invalid tenant id, bad request.
            if (string.IsNullOrEmpty(model.TenantId) || !Guid.TryParse(model.TenantId, out var tenantId) || tenantId == default)
                return BadRequest("Invalid Tenant Id.");

            // Create and save the user.
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = $"{model.TenantId}_{model.Email}",
                TenantId = tenantId
            };
            var result = await userManager.CreateAsync(user, model.Password).ConfigureAwait(false);


            // Return result of user creation.
            return result.Succeeded ? Ok(user) : StatusCode(500, result.Errors);
        }

        /// <summary>
        /// Varifies password strength against regex in Config DB
        /// </summary>
        /// <param name="password">The password for which we check strength</param>
        /// <param name="tenantId">The company for which we check password strength</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ValidatePasswordStrength(string password, string tenantId)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(tenantId))
                return View("Error");

            var config = await identityB2CDbContext.Configs
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name == ConfigVariableType.PasswordStrength.ToString() && c.TenantId == new Guid(tenantId));

            if (config != null && !string.IsNullOrEmpty(config.Value)
                && !string.IsNullOrEmpty(config.Description))
            {
                var rgx = new Regex(config.Value);
                if (rgx.IsMatch(password))
                {
                    return Json(true);
                }
                else
                {
                    return Json(config.Description);
                }
            }
            else
            {
                // Default password strength, basic 6 characters.
                if (password.Length < 6)
                    return Json("Password must be at least 6 characters.");
            }

            return Json(true);
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
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                string pwdCode = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("SetPassword", "Account", values: new { email = user.Email, code = pwdCode }, Request.Scheme);
                var email = user.Email;
                var userId = user.Id;

                await accountHelper.SendResetPasswordLink(callbackUrl, userId.ToString(), email);
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
        /// <param name="redirectUrl">Redirect Url after account confirmation.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult> ConfirmAccount(string userId, string code, string redirectUrl = null)
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
                    return RedirectToAction("SetPassword", new SetPasswordViewModel() { Email = user.Email, Code = pwdRecoveryCode, RedirectUrl = redirectUrl });
            }

            var result = await userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                return View("AccountConfirmed");
            }
            return RedirectToAction("SetPassword", new SetPasswordViewModel() { Email = user.Email, Code = pwdRecoveryCode, RedirectUrl = redirectUrl });
        }

        /// <summary>
        /// Builds a SetPasswordViewModel and returns the set password View.
        /// </summary>
        /// <param name="email">Email of the user.</param>
        /// <param name="code">Code for setting password.</param>
        /// <param name="redirectUrl">Redirect Url after setting up password.</param>
        /// <returns>The set password view for user to enter email, password, confirm password and code.</returns>
        [HttpGet("[action]")]
        public IActionResult SetPassword(string email, string code, string redirectUrl = null)
        {
            return View(new SetPasswordViewModel { Email = email, Code = code, RedirectUrl = redirectUrl });
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

                if (user == null)
                    return View("Error");

                // Validate that the code provided is for the correct user.
                if (!await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider,
                    "ResetPassword", model.Code))
                {
                    logger.LogCritical($"Could not validate email confirmation.  Returning set password confirmation.");
                    return View("SetPasswordConfirmation");
                }

                // TODO: [ap] show the errors when setting a password
                if (!await userManager.HasPasswordAsync(user).ConfigureAwait(false))
                {
                    await userManager.RemovePasswordAsync(user).ConfigureAwait(false);
                    _ = await userManager.AddPasswordAsync(user, model.Password).ConfigureAwait(false);
                }
                else
                {
                    _ = await userManager.ResetPasswordAsync(user, model.Code, model.Password).ConfigureAwait(false);
                }

                tenantViewModel = await tenantService.GetTenantById(user.TenantId.ToString()).ConfigureAwait(false);
                await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            }
            if (!string.IsNullOrEmpty(model.RedirectUrl))
                return Redirect(model.RedirectUrl);

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
