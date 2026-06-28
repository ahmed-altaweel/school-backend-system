using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Contracts;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshToken : GenericRepository<UserRefreshToken>, IRefreshToken
    {


        public RefreshToken(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

    }
}
