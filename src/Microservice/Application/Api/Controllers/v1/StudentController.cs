using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonoRepo.Framework.Extensions.Controllers;
using MonoRepo.Framework.Infrastructure.Models;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Student.AddStudent;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Student.RemoveStudent;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Student.UpdateStudent;
using MonoRepo.Microservice.Application.Domain.Entities;
using MonoRepo.Microservice.Application.Query.GetPagedStudents;
using MonoRepo.Microservice.Application.Query.GetStudentById;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns><see cref="PagedResult{T}"/> of <see cref="StudentSummaryViewModel"/>.</returns>
        [ProducesResponseType(typeof(PagedResult<StudentSummaryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("paged-result")]
        public async Task<IActionResult> GetPagedStudents([FromBody] GetPagedStudentsQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        /// <summary>
        /// Retrieves student for a given student Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="GetStudentByIdViewModel"/></returns>
        [ProducesResponseType(typeof(GetStudentByIdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            return Ok(await mediator.Send(new GetStudentByIdQuery { Id = id }));
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Id of the new student.</returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        /// <summary>
        /// Updates an existing student.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(UpdateStudentCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        /// <summary>
        /// Removes an student.
        /// </summary>
        /// <param name="id">Id of the <see cref="Student"/> to mark deleted.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStudent([FromRoute] int id)
        {
            return Ok(await mediator.Send(new RemoveStudentCommand { Id = id }));
        }
    }
}
