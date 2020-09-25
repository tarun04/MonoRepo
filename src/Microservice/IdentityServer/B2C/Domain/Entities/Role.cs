using Microsoft.AspNetCore.Identity;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Guid ProductId { get; set; }
        public bool IsProductDefault { get; set; }
    }
}
