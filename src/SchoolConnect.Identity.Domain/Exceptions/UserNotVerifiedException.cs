namespace SchoolConnect.Identity.Domain.Exceptions;

public class UserNotVerifiedException : Exception
{
    public UserNotVerifiedException() 
        : base("User email is not verified") { }

    public UserNotVerifiedException(string message) 
        : base(message) { }
}
