using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Identity.Domain.Enums;
using SchoolConnect.Identity.Domain.ValueObjects;

namespace SchoolConnect.Identity.Domain.Entities;

public class UserProfile : Entity
{
    public Guid UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? DisplayName { get; private set; }
    public string? AvatarUrl { get; private set; }
    public string? Bio { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public Gender? Gender { get; private set; }
    public string? PreferredLanguage { get; private set; }
    public string? Timezone { get; private set; }
    public Address? Address { get; private set; }

    private UserProfile() { }

    public UserProfile(Guid userId, string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        DisplayName = $"{firstName} {lastName}";
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string firstName, string lastName, string? displayName, string? bio, 
        DateTime? dateOfBirth, Gender? gender, string? preferredLanguage, string? timezone, Address? address)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        DisplayName = displayName ?? $"{firstName} {lastName}";
        Bio = bio;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        PreferredLanguage = preferredLanguage;
        Timezone = timezone;
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateAvatar(string avatarUrl)
    {
        AvatarUrl = avatarUrl;
        UpdatedAt = DateTime.UtcNow;
    }
}
