using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonoRepo.Framework.Extensions.Attributes;
using MonoRepo.Microservice.IdentityServer.B2B.Command.AddUser;
using MonoRepo.Microservice.IdentityServer.B2B.Command.DeleteUser;
using MonoRepo.Microservice.IdentityServer.B2B.Command.UpdateUser;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using MonoRepo.Microservice.IdentityServer.B2B.Query.GetUser;
using MonoRepo.Microservice.IdentityServer.B2B.Query.GetUsers;
using MonoRepo.Microservice.IdentityServer.B2B.Query.GetUsersWithRoles;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Controllers
{
    [IdentityUserFilter]
    [Route("[controller]")]
    public class UserManagementController : Controller
    {
        private readonly IMediator mediator;

        public UserManagementController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("user")]
        [ProducesResponseType(typeof(IEnumerable<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            return Ok(await mediator.Send(new GetUsersWithRolesQuery()).ConfigureAwait(false));
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUser([FromRoute] Guid userId)
        {
            return Ok(await mediator.Send(new GetUserQuery() { UserId = userId }).ConfigureAwait(false));
        }

        [HttpGet("user/claim/{claimName}/permission/{permissionType}")]
        [ProducesResponseType(typeof(IEnumerable<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUsers([FromRoute] string permissionType, [FromRoute] string claimName)
        {
            return Ok(await mediator.Send(new GetUsersQuery
            {
                PermissionType = permissionType,
                ClaimName = claimName
            }).ConfigureAwait(false));
        }

        [HttpPost("user")]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            return Ok(await mediator.Send(command).ConfigureAwait(false));
        }

        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            return Ok(await mediator.Send(command).ConfigureAwait(false));
        }

        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            return Ok(await mediator.Send(new DeleteUserCommand { Id = userId }).ConfigureAwait(false));
        }
    }
}
