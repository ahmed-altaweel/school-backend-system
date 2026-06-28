using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Users.Command.Models;
using SchoolProject.Core.Features.Users.Query.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand user)
        {
            return Result(await Mediator.Send(user));
        }
        [HttpGet]
        public async Task<IActionResult> GetPaginationUser([FromQuery] GetUserListQuery user)
        {
            return Ok(await Mediator.Send(user));
        }
    }
}
