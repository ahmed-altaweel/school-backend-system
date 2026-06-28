using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Emails.Command.Model;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand email)
        {
            return Result(await Mediator.Send(email));
        }
    }
}
