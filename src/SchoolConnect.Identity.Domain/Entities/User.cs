using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Identity.Domain.Enums;
using SchoolConnect.Identity.Domain.Events;
using SchoolConnect.Identity.Domain.Exceptions;

namespace SchoolConnect.Identity.Domain.Entities;

public class User : AggregateRoot
{
    public string Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string PasswordHash { get; private set; }
    public bool EmailVerified { get; private set; }
    public bool PhoneVerified { get; private set; }
    public bool MfaEnabled { get; private set; }
    public string? MfaSecret { get; private set; }
    public UserStatus Status { get; private set; }
    public UserType Type { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public int FailedLoginAttempts { get; private set; }
    public DateTime? LockoutEnd { get; private set; }
    
    public UserProfile? Profile { get; private set; }
    public List<Guid> RoleIds { get; private set; } = [];

    private User() { }

    public User(string email, string passwordHash, UserType type, string firstName, string lastName, string? phoneNumber = null)
    {
        Id = Guid.NewGuid();
        Email = email?.ToLowerInvariant() ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Type = type;
        PhoneNumber = phoneNumber;
        Status = UserStatus.PendingVerification;
        EmailVerified = false;
        PhoneVerified = false;
        MfaEnabled = false;
        FailedLoginAttempts = 0;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Profile = new UserProfile(Id, firstName, lastName);

        Apply(new UserRegisteredEvent(Id, Email, Type.ToString(), Version));
    }

    public void VerifyEmail()
    {
        if (!EmailVerified)
        {
            EmailVerified = true;
            if (Status == UserStatus.PendingVerification)
            {
                Status = UserStatus.Active;
            }
            UpdatedAt = DateTime.UtcNow;
            Apply(new UserVerifiedEvent(Id, Email, "Email", Version));
        }
    }

    public void VerifyPhone()
    {
        if (!PhoneVerified && PhoneNumber != null)
        {
            PhoneVerified = true;
            UpdatedAt = DateTime.UtcNow;
            Apply(new UserVerifiedEvent(Id, Email, "Phone", Version));
        }
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash ?? throw new ArgumentNullException(nameof(newPasswordHash));
        UpdatedAt = DateTime.UtcNow;
        Apply(new PasswordChangedEvent(Id, Version));
    }

    public void EnableMfa(string mfaSecret)
    {
        MfaSecret = mfaSecret ?? throw new ArgumentNullException(nameof(mfaSecret));
        MfaEnabled = true;
        UpdatedAt = DateTime.UtcNow;
        Apply(new MfaEnabledEvent(Id, Version));
    }

    public void DisableMfa()
    {
        MfaEnabled = false;
        MfaSecret = null;
        UpdatedAt = DateTime.UtcNow;
        Apply(new MfaDisabledEvent(Id, Version));
    }

    public void RecordLogin(string ipAddress, string deviceInfo)
    {
        LastLoginAt = DateTime.UtcNow;
        FailedLoginAttempts = 0;
        LockoutEnd = null;
        UpdatedAt = DateTime.UtcNow;
        Apply(new UserLoggedInEvent(Id, ipAddress, deviceInfo, Version));
    }

    public void RecordFailedLogin()
    {
        FailedLoginAttempts++;
        UpdatedAt = DateTime.UtcNow;

        if (FailedLoginAttempts >= 5)
        {
            LockoutEnd = DateTime.UtcNow.AddMinutes(30);
            Status = UserStatus.Locked;
        }
    }

    public void Unlock()
    {
        FailedLoginAttempts = 0;
        LockoutEnd = null;
        if (Status == UserStatus.Locked)
        {
            Status = UserStatus.Active;
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public void Suspend()
    {
        Status = UserStatus.Suspended;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (EmailVerified)
        {
            Status = UserStatus.Active;
            FailedLoginAttempts = 0;
            LockoutEnd = null;
            UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            throw new UserNotVerifiedException("Cannot activate unverified user");
        }
    }

    public void Deactivate()
    {
        Status = UserStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRole(Guid roleId)
    {
        if (!RoleIds.Contains(roleId))
        {
            RoleIds.Add(roleId);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveRole(Guid roleId)
    {
        if (RoleIds.Remove(roleId))
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public bool IsLocked => Status == UserStatus.Locked && LockoutEnd.HasValue && LockoutEnd > DateTime.UtcNow;
    public bool IsActive => Status == UserStatus.Active && !IsLocked;

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - apply events to rebuild state
    }
}
