using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Contracts;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static void AddInfrastructureDependencies(this IServiceCollection service)
        {
            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<IStudentRepository, StudentRepository>();
            service.AddTransient<IRefreshToken, RefreshToken>();

        }
    }
}