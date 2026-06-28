using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Service.Contracts;
using SchoolProject.Service.Implementaion;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static void AddServiceDependency(this IServiceCollection service)
        {
            service.AddTransient<IStudentService, StudentService>();
            service.AddTransient<IAuthenticationService, AuthenticationService>();
            service.AddSingleton<ConcurrentDictionary<string, RefreshToken>>();
            service.AddTransient<IEmailService, EmailService>();



        }
    }
}