using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonoRepo.Framework.Extensions.Controllers;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.AddInstructor;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.RemoveInstructor;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.UpdateInstructor;
using MonoRepo.Microservice.Application.Domain.Entities;
using MonoRepo.Microservice.Application.Query.GetInstructorById;
using MonoRepo.Microservice.Application.Query.GetInstructors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InstructorController : BaseController
    {
        /// <summary>
        /// Retrieves all instructors.
        /// </summary>
        /// <returns><see cref="List{T}"/> of <see cref="GetInstructorsViewModel"/>.</returns>
        [ProducesResponseType(typeof(List<GetInstructorsViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetInstructors([FromBody] GetInstructorsQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        /// <summary>
        /// Retrieves instructor for a given instructor Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="GetInstructorByIdViewModel"/></returns>
        [ProducesResponseType(typeof(GetInstructorByIdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstructorById(int id)
        {
            return Ok(await mediator.Send(new GetInstructorByIdQuery { Id = id }));
        }

        /// <summary>
        /// Adds a new instructor.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Id of the new instructor.</returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddInstructor(AddInstructorCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        /// <summary>
        /// Updates an existing instructor.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdateInstructor(UpdateInstructorCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        /// <summary>
        /// Removes an instructor.
        /// </summary>
        /// <param name="id">Id of the <see cref="Instructor"/> to mark deleted.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveInstructor([FromRoute] int id)
        {
            return Ok(await mediator.Send(new RemoveInstructorCommand { Id = id }));
        }
    }
}
