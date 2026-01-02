using SchoolConnect.Identity.Application.Services;

namespace SchoolConnect.Identity.Infrastructure.Services;

public class EmailService : IEmailService
{
    // This is a placeholder implementation
    // In a real application, integrate with SendGrid, AWS SES, or similar service

    public async Task SendVerificationEmailAsync(string email, string verificationLink, CancellationToken cancellationToken = default)
    {
        // Log the verification email for now
        Console.WriteLine($"Sending verification email to {email}");
        Console.WriteLine($"Verification link: {verificationLink}");
        await Task.CompletedTask;
    }

    public async Task SendPasswordResetEmailAsync(string email, string resetLink, CancellationToken cancellationToken = default)
    {
        // Log the password reset email for now
        Console.WriteLine($"Sending password reset email to {email}");
        Console.WriteLine($"Reset link: {resetLink}");
        await Task.CompletedTask;
    }

    public async Task SendWelcomeEmailAsync(string email, string firstName, CancellationToken cancellationToken = default)
    {
        // Log the welcome email for now
        Console.WriteLine($"Sending welcome email to {email}");
        Console.WriteLine($"Welcome {firstName}!");
        await Task.CompletedTask;
    }
}
