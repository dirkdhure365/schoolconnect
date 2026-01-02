namespace SchoolConnect.Identity.Application.Services;

public interface IMfaService
{
    string GenerateSecret();
    string GenerateQrCodeUri(string email, string secret);
    bool ValidateCode(string secret, string code);
    string GenerateBackupCodes();
}
