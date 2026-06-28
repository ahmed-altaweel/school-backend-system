using SchoolProject.Core.Features.Users.Result;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserMapping
    {
        void AddGetUserPaginationMapper()
        {
            CreateMap<User, GetUserListResponse>();
        }
    }
}
