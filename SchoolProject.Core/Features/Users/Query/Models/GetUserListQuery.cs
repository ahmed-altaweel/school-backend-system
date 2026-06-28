using MediatR;
using SchoolProject.Core.Features.Users.Result;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Users.Query.Models
{
    public class GetUserListQuery : IRequest<PaginatedResult<GetUserListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
