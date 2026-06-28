namespace SchoolProject.Service.Contracts
{
    public interface IEmailService
    {
        public Task<string> SendEmail(string email, string message);
    }
}
