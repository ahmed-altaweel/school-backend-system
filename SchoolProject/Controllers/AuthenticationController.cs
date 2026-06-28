using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Command.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SigningUser([FromBody] SignInCommand signIn)
        {
            return Result(await Mediator.Send(signIn));
        }
        [HttpPost("refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshToken)
        {
            return Result(await Mediator.Send(refreshToken));
        }

    }
}
