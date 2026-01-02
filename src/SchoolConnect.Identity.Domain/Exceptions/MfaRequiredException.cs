namespace SchoolConnect.Identity.Domain.Exceptions;

public class MfaRequiredException : Exception
{
    public MfaRequiredException() 
        : base("MFA verification is required") { }

    public MfaRequiredException(string message) 
        : base(message) { }
}
