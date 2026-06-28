using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Contracts;
using SchoolProject.Service.Contracts;

namespace SchoolProject.Service.Implementaion
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<string, RefreshTokens> _concurrentDictionary;
        private readonly IRefreshToken _refreshToken;
        private readonly UserManager<User> _userManager;

        public AuthenticationService(JwtSettings jwtSettings,

            IRefreshToken refreshToken, UserManager<User> user)
        {
            _jwtSettings = jwtSettings;
            _concurrentDictionary = new ConcurrentDictionary<string, RefreshTokens>();
            _refreshToken = refreshToken;
            _userManager = user;
        }

        public async Task<JwtAuthResult> GetJwtToken(User user)
        {


            var AccessTokens = GenerateJwtToken(user);
            var refreshToken = new RefreshTokens
            {
                ExpireAt = DateTime.UtcNow.AddDays(2),
                UserName = user.UserName,
                RefreshToken = GenerateToken()

            };
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(2),
                UserId = user.Id,
                IsUsed = true,
                IsRevoked = false,
                JwtId = AccessTokens.jwtToken.Id,
                RefreshToken = refreshToken.RefreshToken,
                Token = AccessTokens.accessToken


            };
            await _refreshToken.AddAsync(userRefreshToken);


            _concurrentDictionary.AddOrUpdate(refreshToken.RefreshToken, refreshToken, (s, t) => refreshToken);
            return new JwtAuthResult { AccessToken = AccessTokens.accessToken, RefreshToken = refreshToken };

        }


        private (JwtSecurityToken jwtToken, string accessToken) GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("UserName",user.UserName),
                new Claim("Email",user.Email),
                new Claim("UserId",user.Id)


           };

            var jwtToken = new JwtSecurityToken(issuer: _jwtSettings.Issuer
                , audience: _jwtSettings.Audience,
                claims: claims,

               expires: DateTime.Now.AddMinutes((double)1),
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256));
            return (jwtToken, new JwtSecurityTokenHandler().WriteToken(jwtToken));
        }
        private string GenerateToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
        {

            var jwtToken = ReadJwtToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                throw new SecurityTokenException("Algorthim is not Used");

            if (jwtToken.ValidTo > DateTime.UtcNow)
                throw new SecurityTokenException("Token is not excpird");

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value;

            var user = await _refreshToken.GetTableNoTracking()
                .FirstOrDefaultAsync(x =>
                x.RefreshToken == refreshToken && x.UserId == userId);

            if (user == null)
                throw new SecurityTokenException("the refresh token not exist");

            if (user.ExpireDate < DateTime.UtcNow)
            {
                user.IsRevoked = true;
                user.IsUsed = false;
                await _refreshToken.UpdateAsync(user);
                throw new SecurityTokenException("Refresh token is expired");
            }
            var userResult = await _userManager.FindByIdAsync(userId);


            var token = GenerateJwtToken(userResult);
            var refreshTokens = new RefreshTokens
            {
                UserName = userResult.UserName,
                RefreshToken = refreshToken,
                ExpireAt = user.ExpireDate
            };
            return new JwtAuthResult { AccessToken = token.accessToken, RefreshToken = refreshTokens };

        }

        private JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException();
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);

            return response;
        }

        public Task<string> ValidateToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException();
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            var parameter = new TokenValidationParameters
            {
                ValidAudience = _jwtSettings.Audience,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidateLifetime = _jwtSettings.ValidateLifetime,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSingingKey


            };
            var validator = handler.ValidateToken(accessToken, parameter, out SecurityToken validateToken);
            try
            {
                if (validator == null)
                    throw new SecurityTokenException("Invalid token");
                return Task.FromResult("success");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }
    }
}
