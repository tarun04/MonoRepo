using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoRepo.Framework.Core.Security.ProductPermissions.Application
{
    public class ApplicationPermissions : IPermission
    {
        /// <inheritdoc cref="Permissions" />
        public static string ProductPermissionType = "Application.Permission";

        /// <inheritdoc cref="ProductPermissionType" />
        public static List<string> Permissions = Enum.GetValues(typeof(ApplicationPermissionSet)).Cast<ApplicationPermissionSet>()
            .Select(x => x.ToString()).ToList();
    }
}
