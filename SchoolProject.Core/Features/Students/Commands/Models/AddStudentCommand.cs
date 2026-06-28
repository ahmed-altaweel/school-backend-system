using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand : IRequest<Response<string>>
    {
        // [Required(ErrorMessage ="Student name is required")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }
    }
}
