using SchoolConnect.Identity.Application.Services;

namespace SchoolConnect.Identity.Infrastructure.Services;

public class MfaService : IMfaService
{
    public string GenerateSecret()
    {
        // Generate a random 32-character base32 secret
        var random = new Random();
        const string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        var secret = new char[32];
        for (int i = 0; i < 32; i++)
        {
            secret[i] = base32Chars[random.Next(base32Chars.Length)];
        }
        return new string(secret);
    }

    public string GenerateQrCodeUri(string email, string secret)
    {
        var issuer = "SchoolConnect";
        var encodedIssuer = Uri.EscapeDataString(issuer);
        var encodedEmail = Uri.EscapeDataString(email);
        return $"otpauth://totp/{encodedIssuer}:{encodedEmail}?secret={secret}&issuer={encodedIssuer}";
    }

    public bool ValidateCode(string secret, string code)
    {
        // Simple TOTP validation
        // In a production environment, use a proper TOTP library like OtpNet
        // This is a placeholder implementation
        return !string.IsNullOrEmpty(code) && code.Length == 6;
    }

    public string GenerateBackupCodes()
    {
        // Generate 10 backup codes
        var codes = new List<string>();
        var random = new Random();
        for (int i = 0; i < 10; i++)
        {
            var code = random.Next(100000, 999999).ToString();
            codes.Add(code);
        }
        return string.Join(",", codes);
    }
}
