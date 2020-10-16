﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities;
using MonoRepo.Microservice.IdentityServer.B2C.Infrastructure.EntityConfigurations;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2C.Infrastructure
{
    public partial class IdentityB2CDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Config> Configs { get; set; }

        public IdentityB2CDbContext(DbContextOptions<IdentityB2CDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
    }
}