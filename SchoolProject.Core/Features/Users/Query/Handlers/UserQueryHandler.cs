using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.Users.Query.Models;
using SchoolProject.Core.Features.Users.Result;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.Users.Query.Handlers
{
    public class UserQueryHandler : IRequestHandler<GetUserListQuery, PaginatedResult<GetUserListResponse>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserQueryHandler(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }



        public async Task<PaginatedResult<GetUserListResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.AsQueryable();
            var paginatedResult = await _mapper.ProjectTo<GetUserListResponse>(user).ToPaginateListAsync(request.PageNumber, request.PageSize);
            return paginatedResult;


        }
    }
}
