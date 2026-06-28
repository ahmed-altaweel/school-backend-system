using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Service.Contracts;

namespace SchoolProject.Service.Implementaion
{
    public class EmailService : IEmailService
    {
        public async Task<string> SendEmail(string email, string message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("ahmedaltweel58@gmail.com", "eawa hxbi dxop dkxb");
                    var bodyBuilder = new BodyBuilder
                    {
                        //   HtmlBody = $"<h1>{message}</h1>",

                        TextBody = "ds",
                    };
                    bodyBuilder.Attachments.Add(@"C:\Users\PC\Desktop\set.txt");
                    var Message = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };
                    Message.From.Add(new MailboxAddress("Future Team", "ahmedaltweel58@gmail.com"));
                    Message.To.Add(new MailboxAddress("testing", email));
                    Message.Subject = "new Contact submitted data";
                    await client.SendAsync(Message);
                    client.Disconnect(true);

                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }


            return "Success";
        }
    }
}
