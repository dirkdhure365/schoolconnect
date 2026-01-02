namespace SchoolConnect.Identity.Application.Services;

public interface IEmailService
{
    Task SendVerificationEmailAsync(string email, string verificationLink, CancellationToken cancellationToken = default);
    Task SendPasswordResetEmailAsync(string email, string resetLink, CancellationToken cancellationToken = default);
    Task SendWelcomeEmailAsync(string email, string firstName, CancellationToken cancellationToken = default);
}
