using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Command.Model;
using SchoolProject.Service.Contracts;

namespace SchoolProject.Core.Features.Emails.Command.Handler
{
    public class EmailCommandHandler : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IEmailService _emailService;

        public EmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailService.SendEmail(request.Email, request.Message);

            if (response == "Success")
                return Success(response);
            else
                return BadRequest<string>(response);

        }
    }
}
