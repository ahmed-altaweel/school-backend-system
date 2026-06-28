using SchoolProject.Core.Features.Users.Command.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserMapping
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();
        }
    }
}
