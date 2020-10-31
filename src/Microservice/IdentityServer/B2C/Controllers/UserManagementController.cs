using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonoRepo.Framework.Extensions.Attributes;
using MonoRepo.Microservice.IdentityServer.B2C.Command.AddUser;
using MonoRepo.Microservice.IdentityServer.B2C.Models;
using MonoRepo.Microservice.IdentityServer.B2C.Query.GetUser;
using System.Net;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2C.Controllers
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

        [HttpPost("user")]
        [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUser([FromBody] GetUserQuery query)
        {
            return Ok(await mediator.Send(query).ConfigureAwait(false));
        }

        [HttpPost("user/add")]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            return Ok(await mediator.Send(command).ConfigureAwait(false));
        }
    }
}
