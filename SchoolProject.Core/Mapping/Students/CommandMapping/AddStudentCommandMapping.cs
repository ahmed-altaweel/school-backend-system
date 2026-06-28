using SchoolProject.Core.Features.Students.Commands.Models;
using SchooProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void AddStudentCommandMapping()
        {
            CreateMap<AddStudentCommand, Student>().ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId));
        }
    }
}
