using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Infrastructure
{
    public class Seed
    {
        private static readonly Guid guidDefault = new Guid("b54c9d8f-74f1-482a-a633-604f90f71201");
        private static readonly Guid guidLocalHost = new Guid("2a8c4e05-25cf-4ce1-81b8-6202bcee8a2b");

        public static void EnsureSeedData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<IdentityB2BDbContext>();
            var userMgr = app.ApplicationServices.GetRequiredService<UserManager<User>>();
            var roleMgr = app.ApplicationServices.GetRequiredService<RoleManager<Role>>();

            context.Database.Migrate();
            AddUsers(userMgr);
            AddRoles(roleMgr);
            AddClaims(roleMgr);
            AddRolesToUsers(userMgr);
        }

        private static void AddUsers(UserManager<User> userMgr)
        {
            _ = userMgr.CreateAsync(new User
            {
                Email = "jane@default.com",
                UserName = "jane@default.com",
                FirstName = "Jane",
                LastName = "Doe",
                TenantId = guidDefault,
                IsEnabled = true,
                EmailConfirmed = true
            }, "MonoRepo").Result;

            _ = userMgr.CreateAsync(new User
            {
                Email = "john@localhost.com",
                UserName = "john@localhost.com",
                FirstName = "John",
                LastName = "Doe",
                TenantId = guidLocalHost,
                IsEnabled = true,
                EmailConfirmed = true
            }, "MonoRepo").Result;
        }

        private static void AddRoles(RoleManager<Role> roleMgr)
        {
            var monorepoProduct = new Guid("8e0fa984-38d3-4a6d-aff2-ba4d3a7069ce");

            _ = roleMgr.CreateAsync(new Role
            {
                DisplayName = "Admin",
                ProductId = monorepoProduct,
                IsProductDefault = true,
                TenantId = guidDefault,
                Name = $"Admin_{guidDefault}"
            }).Result;

            _ = roleMgr.CreateAsync(new Role
            {
                DisplayName = "Manager",
                ProductId = monorepoProduct,
                IsProductDefault = true,
                TenantId = guidDefault,
                Name = $"Manager_{guidDefault}"
            }).Result;

            _ = roleMgr.CreateAsync(new Role
            {
                DisplayName = "Admin",
                ProductId = monorepoProduct,
                IsProductDefault = true,
                TenantId = guidLocalHost,
                Name = $"Admin_{guidLocalHost}"
            }).Result;

            _ = roleMgr.CreateAsync(new Role
            {
                DisplayName = "Manager",
                ProductId = monorepoProduct,
                IsProductDefault = true,
                TenantId = guidLocalHost,
                Name = $"Manager_{guidLocalHost}"
            }).Result;
        }

        private static void AddClaims(RoleManager<Role> roleMgr)
        {
            var adminDefault = roleMgr.FindByNameAsync($"Admin_{guidDefault}").Result;
            var managerDefault = roleMgr.FindByNameAsync($"Manager_{guidDefault}").Result;
            var adminLocalHost = roleMgr.FindByNameAsync($"Admin_{guidLocalHost}").Result;
            var managerLocalHost = roleMgr.FindByNameAsync($"Manager_{guidLocalHost}").Result;
        }

        private static void AddRolesToUsers(UserManager<User> userMgr)
        {
            var jane = userMgr.FindByNameAsync("jane@default.com").Result;
            _ = userMgr.AddToRolesAsync(jane, new List<string> {
                $"Admin_{guidDefault}",
                $"Manager_{guidDefault}"
            }).Result;

            var john = userMgr.FindByNameAsync("john@localhost.com").Result;
            _ = userMgr.AddToRolesAsync(john, new List<string> {
                $"Admin_{guidLocalHost}",
                $"Manager_{guidLocalHost}"
            }).Result;
        }
    }
}
