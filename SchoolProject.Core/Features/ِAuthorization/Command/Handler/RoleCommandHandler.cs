using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features._ِAuthorization.Command.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features._ِAuthorization.Command.Handler
{
    public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleCommandHandler(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var identityRole = new IdentityRole();

            identityRole.Name = request.RoleName;
            var role = await _roleManager.CreateAsync(identityRole);

            return Success("Done");
        }
    }
}
