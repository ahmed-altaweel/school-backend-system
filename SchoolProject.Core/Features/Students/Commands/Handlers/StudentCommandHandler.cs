using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Contracts;
using SchooProject.Data.Entities;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>
                                                         , IRequestHandler<EditStudentCommand, Response<string>>
                                                          , IRequestHandler<DeleteStudentCommand, Response<string>>


    {
        private readonly IStudentService _student;
        private readonly IMapper _mapper;

        public StudentCommandHandler(IStudentService student, IMapper mapper)
        {
            _student = student;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapping = _mapper.Map<Student>(request);
            var studentResult = await _student.AddAsync(studentMapping);
            return Success(studentResult);
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {

            var studentResult = await _student.GetStudentByIdAsync(request.Id);

            if (studentResult == null)
                return NotFound<string>("this user not exist");
            var studentMapping = _mapper.Map(request, studentResult);
            await _student.Update(studentMapping);
            return Success("Updated");
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var studentResult = await _student.GetStudentByIdAsync(request.studentId);
            if (studentResult == null)
                return NotFound<string>();
            if (!await _student.DeleteAsync(studentResult))
                return BadRequest<string>("Some thing went error");

            return Success("Deleted");
        }
    }
}
