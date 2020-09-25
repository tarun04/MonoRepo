using Microsoft.AspNetCore.Identity;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
