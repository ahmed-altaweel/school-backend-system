using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.SharedResource;
using SchoolProject.Core.Wrappers;
using SchoolProject.Service.Contracts;
using SchooProject.Data.Entities;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHanlder : ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentResponse>>>,
                                                       IRequestHandler<GetStudentByIdQuery, Response<GetStudentResponse>>,
                                                       IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public StudentQueryHanlder(IStudentService student, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _studentService = student;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<GetStudentResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var StudentList = await _studentService.GetStudentsListAsync();
            var StudentListMapper = _mapper.Map<List<GetStudentResponse>>(StudentList);
            var result = Success(StudentListMapper);
            result.Meta = new { Count = StudentListMapper.Count };
            return result;
        }

        public async Task<Response<GetStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var Student = await _studentService.GetStudentByIdAsync(request.Id);
            if (Student is null)
                return NotFound<GetStudentResponse>(_stringLocalizer[SharedResourcesKeys.Required]);
            var StudentMapper = _mapper.Map<GetStudentResponse>(Student);
            return Success(StudentMapper);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(e.StudentId, e.Name, e.Address, e.Department.DName);
            var querable = _studentService.GetStudentsListQuerable();
            var FilterQuery = _studentService.GetFilterStudentPaginatedQuerable(request.OrderBy, request.Search);
            var studentResult = await FilterQuery.Select(expression).ToPaginateListAsync(request.PageNumber, request.PageSize);
            return studentResult;

        }
    }
}
