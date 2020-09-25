using Microsoft.AspNetCore.Identity;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid TenantId { get; set; }
        public bool IsActive { get; set; }
    }
}
