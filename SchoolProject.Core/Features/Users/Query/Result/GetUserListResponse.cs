namespace SchoolProject.Core.Features.Users.Result
{
    public class GetUserListResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Adress { get; set; }
        public string? Country { get; set; }
    }
}
