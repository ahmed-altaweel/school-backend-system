namespace SchoolProject.Data.Helper
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshTokens RefreshToken { get; set; }
    }

    public class RefreshTokens
    {
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireAt { get; set; }

    }
}
