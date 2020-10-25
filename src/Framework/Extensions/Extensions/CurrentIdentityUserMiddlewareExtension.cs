using Microsoft.AspNetCore.Builder;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Framework.Extensions.Middleware;

namespace MonoRepo.Framework.Extensions.Extensions
{
    public static class CurrentIdentityUserMiddlewareExtension
    {
        /// <summary>
        /// Extension method for registering <see cref="CurrentIdentityUserMiddleware"/> that populates 
        /// the <see cref="IdentityUser"/> object in DI.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/> for fluent API.</returns>
        public static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app)
        {
            app.UseMiddleware<CurrentIdentityUserMiddleware>();
            return app;
        }
    }
}
