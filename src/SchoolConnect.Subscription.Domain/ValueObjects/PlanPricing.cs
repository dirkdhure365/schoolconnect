using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Subscription.Domain.ValueObjects;

public class PlanPricing : ValueObject
{
    public decimal MonthlyPrice { get; private set; }
    public decimal AnnualPrice { get; private set; }
    public string Currency { get; private set; }

    private PlanPricing()
    {
        Currency = string.Empty;
    }

    public PlanPricing(decimal monthlyPrice, decimal annualPrice, string currency)
    {
        MonthlyPrice = monthlyPrice;
        AnnualPrice = annualPrice;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return MonthlyPrice;
        yield return AnnualPrice;
        yield return Currency;
    }
}
