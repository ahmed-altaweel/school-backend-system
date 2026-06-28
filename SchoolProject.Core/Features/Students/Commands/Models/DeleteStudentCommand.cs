using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class DeleteStudentCommand : IRequest<Response<string>>
    {
        public int studentId { get; set; }

        public DeleteStudentCommand(int id)
        {
            studentId = id;
        }
    }
}
