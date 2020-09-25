using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public Guid TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEnabled { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
