using AutoMapper;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserMapping : Profile
    {
        public UserMapping()
        {
            AddUserMapping();
            AddGetUserPaginationMapper();
        }
    }
}
