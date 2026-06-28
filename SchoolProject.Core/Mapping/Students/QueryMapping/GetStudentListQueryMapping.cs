using SchoolProject.Core.Features.Students.Queries.Results;
using SchooProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {

        public void GetStudetnListQueryMapping()
        {
            CreateMap<Student, GetStudentResponse>()
               .ForMember(dest => dest.DName, opt => opt.MapFrom(src => src.Department.DName));
        }
    }
}
