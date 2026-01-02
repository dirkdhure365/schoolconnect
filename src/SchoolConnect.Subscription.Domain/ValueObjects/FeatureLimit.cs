using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Subscription.Domain.ValueObjects;

public class FeatureLimit : ValueObject
{
    public int Value { get; private set; }
    public bool IsUnlimited { get; private set; }

    private FeatureLimit() { }

    public FeatureLimit(int value, bool isUnlimited = false)
    {
        Value = value;
        IsUnlimited = isUnlimited;
    }

    public static FeatureLimit Unlimited() => new FeatureLimit(0, true);
    
    public static FeatureLimit Limited(int value) => new FeatureLimit(value, false);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return IsUnlimited;
    }
}
