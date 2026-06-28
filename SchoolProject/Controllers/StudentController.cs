using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    //[Route(Router.StudentRouting.Prefix)]
    [ApiController]

    public class StudentController : AppControllerBase
    {


        [HttpGet(Router.StudentRouting.List)]
        [Authorize]
        public async Task<IActionResult> GetStudentList()
        {
            return Result(await Mediator.Send(new GetStudentListQuery()));
        }
        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentById([FromRoute] int Id)
        {
            return Result(await Mediator.Send(new GetStudentByIdQuery() { Id = Id }));
        }
        [Authorize(Policy = "CreateStudent")]
        [HttpPost(Router.StudentRouting.Prefix)]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand student)
        {
            try
            {
                return Result(await Mediator.Send(student));
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut(Router.StudentRouting.Prefix)]
        public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand sudent)
        {
            return Result(await Mediator.Send(sudent));
        }

        [HttpDelete(Router.StudentRouting.GetById)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int Id)
        {
            return Result(await Mediator.Send(new DeleteStudentCommand(Id)));
        }

        [HttpGet(Router.StudentRouting.Prefix + "/paginated")]

        public async Task<IActionResult> GetPaginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
