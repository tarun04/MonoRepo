﻿using Microsoft.AspNetCore.Builder;
using MonoRepo.Framework.Extensions.Middleware;

namespace MonoRepo.Framework.Extensions.Extensions
{
    public static class RequestProductInfoMiddlewareExtension
    {
        /// <summary>
        /// Extension for adding the <see cref="RequestingProductInfoMiddleware"/> to the DI.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/> for fluent API.</returns>
        public static IApplicationBuilder UseRequestProductInfo(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestingProductInfoMiddleware>();
            return app;
        }
    }
}
