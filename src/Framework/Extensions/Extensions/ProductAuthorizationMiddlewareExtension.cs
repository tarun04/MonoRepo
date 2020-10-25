using Microsoft.AspNetCore.Builder;
using MonoRepo.Framework.Extensions.Middleware;

namespace MonoRepo.Framework.Extensions.Extensions
{
    public static class ProductAuthorizationMiddlewareExtension
    {
        /// <summary>
        /// Extension for adding the <see cref="ProductAuthorizationMiddleware"/> to the middleware pipeline.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/> for fluent API.</returns>
        public static IApplicationBuilder UseProductAuthorization(this IApplicationBuilder app)
        {
            app.UseMiddleware<ProductAuthorizationMiddleware>();
            return app;
        }
    }
}
