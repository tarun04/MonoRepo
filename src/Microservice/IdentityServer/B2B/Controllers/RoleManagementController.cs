using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonoRepo.Framework.Extensions.Attributes;
using MonoRepo.Microservice.IdentityServer.B2B.Command.AddRole;
using MonoRepo.Microservice.IdentityServer.B2B.Command.DeleteRole;
using MonoRepo.Microservice.IdentityServer.B2B.Command.UpdateRole;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using MonoRepo.Microservice.IdentityServer.B2B.Query.GetPermissions;
using MonoRepo.Microservice.IdentityServer.B2B.Query.GetRoles;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Controllers
{
    [IdentityUserFilter]
    [Route("[controller]")]
    public class RoleManagementController : Controller
    {
        private readonly IMediator mediator;

        public RoleManagementController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("role")]
        [ProducesResponseType(typeof(IEnumerable<RoleViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await mediator.Send(new GetRolesQuery()).ConfigureAwait(false));
        }

        [HttpGet("permission")]
        [ProducesResponseType(typeof(IEnumerable<RoleClaimViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPermissions()
        {
            return Ok(await mediator.Send(new GetPermissionsQuery()).ConfigureAwait(false));
        }

        [HttpPost("role")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
        {
            if (!HttpContext.Request.Headers.TryGetValue("Requesting-ProductId", out var productId))
                return StatusCode(500);

            command.ProductId = Guid.Parse(productId);

            return Ok(await mediator.Send(command).ConfigureAwait(false));
        }

        [HttpPut("role")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command)
        {
            return Ok(await mediator.Send(command).ConfigureAwait(false));
        }

        [HttpDelete("role/{roleId}")]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid roleId)
        {
            return Ok(await mediator.Send(new DeleteRoleCommand { Id = roleId }).ConfigureAwait(false));
        }
    }
}
