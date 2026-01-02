using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Subscription.Domain.Enums;

namespace SchoolConnect.Subscription.Domain.ValueObjects;

public class BillingCycle : ValueObject
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public BillingFrequency BillingFrequency { get; private set; }

    private BillingCycle() { }

    public BillingCycle(DateTime startDate, DateTime endDate, BillingFrequency billingFrequency)
    {
        StartDate = startDate;
        EndDate = endDate;
        BillingFrequency = billingFrequency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
        yield return BillingFrequency;
    }
}
