using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.Features.Authentication.Command.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
