using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolProject.Core
{
    public static class ModuleCoreDependencies
    {
        public static void AddCoreDependencies(this IServiceCollection service)
        {

            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            service.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            // service.AddValidatorsFromAssemblyContaining<AddUserValidatorCommand>();
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //   service.AddValidatorsFromAssemblyContaining<EditStudentValidatorCommand>();
        }
    }
}