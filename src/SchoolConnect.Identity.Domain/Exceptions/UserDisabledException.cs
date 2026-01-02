namespace SchoolConnect.Identity.Domain.Exceptions;

public class UserDisabledException : Exception
{
    public UserDisabledException() 
        : base("User account is disabled") { }

    public UserDisabledException(string message) 
        : base(message) { }
}
