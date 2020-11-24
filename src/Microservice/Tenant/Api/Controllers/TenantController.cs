using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonoRepo.Framework.Extensions.Controllers;
using MonoRepo.Microservice.Tenant.Domain.ViewModel;
using MonoRepo.Microservice.Tenant.Query.GetTenantById;
using MonoRepo.Microservice.Tenant.Query.GetTenantByName;
using System;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Tenant.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TenantController : BaseController
    {
        /// <summary>
        /// Returns the tenant that matches the provided id.
        /// </summary>
        /// <param name="id">Id of the tenant to get.</param>
        /// <returns><see cref="Tenant"/> holds information about the tenant.</returns>
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TenantViewModel), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenantById([FromRoute] Guid id)
        {
            return Ok(await mediator.Send(new GetTenantByIdQuery { Id = id }).ConfigureAwait(false));
        }

        /// <summary>
        /// Returns the tenant that matches the provided name.
        /// </summary>
        /// <param name="name">Name of the tenant to get.</param>
        /// <returns><see cref="Tenant"/> holds information about the tenant.</returns>
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TenantViewModel), StatusCodes.Status400BadRequest)]
        [HttpGet("{name}/name")]
        public async Task<IActionResult> GetTenantByName([FromRoute] string name)
        {
            return Ok(await mediator.Send(new GetTenantByNameQuery { Name = name }).ConfigureAwait(false));
        }
    }
}
