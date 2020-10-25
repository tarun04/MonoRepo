using Microsoft.AspNetCore.Builder;
using MonoRepo.Framework.Extensions.Middleware;

namespace MonoRepo.Framework.Extensions.Extensions
{
    public static class IdentityUserClaimsForwardingMiddlewareExtension
    {
        /// <summary>
        /// Extension method for registering <see cref="IdentityUserClaimsForwardingMiddleware"/> in DI.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/> for fluent API.</returns>
        public static IApplicationBuilder UseIdentityUserClaimsForwarding(this IApplicationBuilder app)
        {
            app.UseMiddleware<IdentityUserClaimsForwardingMiddleware>();
            return app;
        }
    }
}
