using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.ValueObjects;

public class Password : ValueObject
{
    public string Hash { get; private set; }

    private Password(string hash)
    {
        Hash = hash;
    }

    public static Password Create(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
            throw new ArgumentException("Password cannot be empty", nameof(plainPassword));

        if (plainPassword.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters long", nameof(plainPassword));

        return new Password(BCrypt.Net.BCrypt.HashPassword(plainPassword));
    }

    public static Password FromHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash cannot be empty", nameof(hash));

        return new Password(hash);
    }

    public bool Verify(string plainPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, Hash);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hash;
    }
}
