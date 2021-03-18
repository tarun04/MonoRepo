using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonoRepo.Framework.Extensions.Controllers;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.AddParent;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.RemoveParent;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.UpdateParent;
using MonoRepo.Microservice.Application.Query.GetParentById;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParentController : BaseController
    {
        /// <summary>
        /// Retrieves parent for a given parent Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="GetParentByIdViewModel"/></returns>
        [ProducesResponseType(typeof(GetParentByIdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetParentById(int id)
        {
            return Ok(await mediator.Send(new GetParentByIdQuery { Id = id }));
        }

        /// <summary>
        /// Adds a new parent.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Id of the new parent.</returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddParent(AddParentCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        /// <summary>
        /// Updates an existing parent.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdateParent(UpdateParentCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        /// <summary>
        /// Removes an parent.
        /// </summary>
        /// <param name="id">Id of the <see cref="Parent"/> to mark deleted.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveParent([FromRoute] int id)
        {
            return Ok(await mediator.Send(new RemoveParentCommand { Id = id }));
        }
    }
}
