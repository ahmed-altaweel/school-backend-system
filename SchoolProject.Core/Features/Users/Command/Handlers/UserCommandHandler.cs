using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Command.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.Users.Command.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserCommandHandler(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identity = _mapper.Map<User>(request);
            var createUser = await _userManager.CreateAsync(identity, request.Password);
            if (!createUser.Succeeded)
            {
                var errors = string.Join("|", createUser.Errors.Select(x => x.Description));
                return BadRequest<string>(errors);
            }

            return Created("created");
        }
    }
}
