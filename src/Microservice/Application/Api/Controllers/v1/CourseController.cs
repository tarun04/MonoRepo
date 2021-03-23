using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonoRepo.Framework.Extensions.Controllers;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Course.AddCourse;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Course.RemoveCourse;
using MonoRepo.Microservice.Application.Domain.Entities;
using MonoRepo.Microservice.Application.Query.GetCourses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CourseController : BaseController
    {
        /// <summary>
        /// Retrieves all courses.
        /// </summary>
        /// <returns><see cref="List{T}"/> of <see cref="GetCoursesViewModel"/>.</returns>
        [ProducesResponseType(typeof(List<GetCoursesViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromBody] GetCoursesQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        /// <summary>
        /// Adds a new course.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Id of the new course.</returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        /// <summary>
        /// Removes an course.
        /// </summary>
        /// <param name="id">Id of the <see cref="Course"/> to mark deleted.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCourse([FromRoute] int id)
        {
            return Ok(await mediator.Send(new RemoveCourseCommand { Id = id }));
        }
    }
}
