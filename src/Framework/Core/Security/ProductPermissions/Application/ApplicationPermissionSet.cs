using System.ComponentModel.DataAnnotations;

namespace MonoRepo.Framework.Core.Security.ProductPermissions.Application
{
    public enum ApplicationPermissionSet
    {
        [Display(GroupName = "Full Access", Name = "Full Access")]
        FullAccess
    }
}
