using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid TenantId { get; set; }
        public string DisplayName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsProductDefault { get; set; }

        public ICollection<IdentityRoleClaim<Guid>> Claims { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
