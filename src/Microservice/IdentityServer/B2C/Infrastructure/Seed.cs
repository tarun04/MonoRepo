using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2C.Infrastructure
{
    public class Seed
    {
        public static readonly Guid guidDefault = new Guid("5c3ffee9-7e1a-47bf-bf4f-42b1b6e868c6");
        public static readonly Guid guidLocalHost = new Guid("1794298e-5e0a-4213-8700-a46c3a906da6");

        public static void EnsureSeedData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<IdentityB2CDbContext>();
            var userMgr = app.ApplicationServices.GetRequiredService<UserManager<User>>();

            context.Database.Migrate();
            AddUsers(userMgr);
        }

        private static void AddUsers(UserManager<User> userMgr)
        {
            _ = userMgr.CreateAsync(new User
            {
                Email = "jane@default.com",
                UserName = "56f55034-75e7-4c61-bb6a-df2d4ba3d96b-jane@default.com",
                FirstName = "Jane",
                LastName = "Doe",
                TenantId = guidDefault,
                EmailConfirmed = true
            }, "MonoRepo").Result;

            _ = userMgr.CreateAsync(new User
            {
                Email = "john@localhost.com",
                UserName = "f8780ece-6dce-4e29-9a0f-a77cb523f791-john@localhost.com",
                FirstName = "John",
                LastName = "Doe",
                TenantId = guidLocalHost,
                EmailConfirmed = true
            }, "MonoRepo").Result;
        }
    }
}
