using Bookify.Application.Abstractions.Email;

namespace Bookify.Infrustructure.Email
{
    internal sealed class EmailServices : IEmailService
    {
        public Task SendAsync(Domain.Users.Email recipient, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}
