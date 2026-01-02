using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Billing.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    private Money()
    {
        Currency = string.Empty;
    }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}

public class BillingAddress : ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PostalCode { get; private set; }
    public string Country { get; private set; }

    private BillingAddress()
    {
        Street = string.Empty;
        City = string.Empty;
        State = string.Empty;
        PostalCode = string.Empty;
        Country = string.Empty;
    }

    public BillingAddress(string street, string city, string state, string postalCode, string country)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        Country = country ?? throw new ArgumentNullException(nameof(country));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return PostalCode;
        yield return Country;
    }
}

public class CardDetails : ValueObject
{
    public string Last4 { get; private set; }
    public string Brand { get; private set; }
    public int ExpiryMonth { get; private set; }
    public int ExpiryYear { get; private set; }

    private CardDetails()
    {
        Last4 = string.Empty;
        Brand = string.Empty;
    }

    public CardDetails(string last4, string brand, int expiryMonth, int expiryYear)
    {
        Last4 = last4 ?? throw new ArgumentNullException(nameof(last4));
        Brand = brand ?? throw new ArgumentNullException(nameof(brand));
        ExpiryMonth = expiryMonth;
        ExpiryYear = expiryYear;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Last4;
        yield return Brand;
        yield return ExpiryMonth;
        yield return ExpiryYear;
    }
}

public class BankDetails : ValueObject
{
    public string BankName { get; private set; }
    public string AccountLast4 { get; private set; }
    public string? AccountType { get; private set; }

    private BankDetails()
    {
        BankName = string.Empty;
        AccountLast4 = string.Empty;
    }

    public BankDetails(string bankName, string accountLast4, string? accountType = null)
    {
        BankName = bankName ?? throw new ArgumentNullException(nameof(bankName));
        AccountLast4 = accountLast4 ?? throw new ArgumentNullException(nameof(accountLast4));
        AccountType = accountType;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return BankName;
        yield return AccountLast4;
        if (AccountType != null) yield return AccountType;
    }
}

public class MobileMoneyDetails : ValueObject
{
    public string Provider { get; private set; }
    public string PhoneNumberLast4 { get; private set; }

    private MobileMoneyDetails()
    {
        Provider = string.Empty;
        PhoneNumberLast4 = string.Empty;
    }

    public MobileMoneyDetails(string provider, string phoneNumberLast4)
    {
        Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        PhoneNumberLast4 = phoneNumberLast4 ?? throw new ArgumentNullException(nameof(phoneNumberLast4));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Provider;
        yield return PhoneNumberLast4;
    }
}
