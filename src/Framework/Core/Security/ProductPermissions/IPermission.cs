using System.Collections.Generic;

namespace MonoRepo.Framework.Core.Security.ProductPermissions
{
    public interface IPermission
    {
        /// <summary>
        /// Name of the product permission type.
        /// This needs to be {{Product name in Product Database}}.Permission.
        /// </summary>
        static string ProductPermissionType;

        /// <summary>
        /// List of enum values for the product permission set.
        /// </summary>
        static List<string> Permissions;
    }
}
