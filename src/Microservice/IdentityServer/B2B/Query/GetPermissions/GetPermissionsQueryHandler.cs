using MediatR;
using MonoRepo.Framework.Core.Security.ProductPermissions.Application;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetPermissions
{
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<RoleClaimViewModel>>
    {
        public Task<IEnumerable<RoleClaimViewModel>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Enum.GetNames(typeof(ApplicationPermissionSet))
                                       .Select(x => new RoleClaimViewModel
                                       {
                                           RoleClaimType = ApplicationPermissions.ProductPermissionType,
                                           RoleClaimDisplayName = typeof(ApplicationPermissionSet).GetMember(x)[0].GetCustomAttribute<DisplayAttribute>().Name,
                                           RoleClaimValue = typeof(ApplicationPermissionSet).GetMember(x)[0].Name
                                       }));
        }
    }
}
