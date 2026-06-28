using SchoolProject.Core.Features.Students.Commands.Models;
using SchooProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
