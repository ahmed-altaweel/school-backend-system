using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Service.Contracts;

namespace SchoolProject.Core.Features.Authentication.Command.Handler
{
    class AuthenticationHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<JwtAuthResult>>
                                                  , IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationHandler(UserManager<User> userManager, SignInManager<User> signInManager,
            IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName));
            if (user == null)
                return NotFound<JwtAuthResult>();
            var signInResult = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!signInResult)
                return BadRequest<JwtAuthResult>();
            var token = await _authenticationService.GetJwtToken(user);
            return Success(token);

        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.GetRefreshToken(request.AccessToken, request.RefreshToken);
            return Success(result);
        }
    }
}
