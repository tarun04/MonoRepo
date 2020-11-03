using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure.EntityConfigurations;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2B.Infrastructure
{
    public partial class IdentityB2BDbContext : IdentityDbContext<
                                                    User,
                                                    Role,
                                                    Guid,
                                                    IdentityUserClaim<Guid>,
                                                    UserRole,
                                                    IdentityUserLogin<Guid>,
                                                    IdentityRoleClaim<Guid>,
                                                    IdentityUserToken<Guid>>
    {
        public IdentityB2BDbContext(DbContextOptions<IdentityB2BDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
