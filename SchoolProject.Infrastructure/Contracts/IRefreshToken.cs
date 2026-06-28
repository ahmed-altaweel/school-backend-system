using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Contracts
{
    public interface IRefreshToken : IGenericRepository<UserRefreshToken>
    {

    }
}
