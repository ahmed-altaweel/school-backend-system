using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;

namespace SchoolProject.Service.Contracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJwtToken(User user);
        public Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken);
        public Task<string> ValidateToken(string accessToken);

    }
}
