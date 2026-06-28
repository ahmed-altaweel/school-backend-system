using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Data.Entities.Identity
{
    public class User : IdentityUser
    {
        User()
        {
            RefreshTokens = new HashSet<UserRefreshToken>();
        }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [InverseProperty("user")]
        public virtual ICollection<UserRefreshToken> RefreshTokens { get; set; }
    }
}
