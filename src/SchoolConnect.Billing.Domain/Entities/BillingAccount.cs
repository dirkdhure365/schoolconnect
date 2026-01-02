using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Billing.Domain.Events;
using SchoolConnect.Billing.Domain.ValueObjects;

namespace SchoolConnect.Billing.Domain.Entities;

public class BillingAccount : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public string BillingName { get; private set; } = string.Empty;
    public string BillingEmail { get; private set; } = string.Empty;
    public BillingAddress Address { get; private set; } = null!;
    public string? TaxId { get; private set; }
    public string? VatNumber { get; private set; }
    public Guid? DefaultPaymentMethodId { get; private set; }
    public string Currency { get; private set; } = "USD";
    public decimal Balance { get; private set; }

    private BillingAccount() { }

    public static BillingAccount Create(
        Guid instituteId,
        string billingName,
        string billingEmail,
        BillingAddress address,
        string currency,
        string? taxId = null,
        string? vatNumber = null)
    {
        var account = new BillingAccount
        {
            InstituteId = instituteId,
            BillingName = billingName,
            BillingEmail = billingEmail,
            Address = address,
            Currency = currency,
            TaxId = taxId,
            VatNumber = vatNumber,
            Balance = 0
        };

        account.AddDomainEvent(new BillingAccountCreatedEvent
        {
            AggregateId = account.Id,
            EventType = nameof(BillingAccountCreatedEvent),
            InstituteId = instituteId,
            BillingEmail = billingEmail
        });

        return account;
    }

    public void Update(
        string billingName,
        string billingEmail,
        BillingAddress address,
        string? taxId,
        string? vatNumber)
    {
        BillingName = billingName;
        BillingEmail = billingEmail;
        Address = address;
        TaxId = taxId;
        VatNumber = vatNumber;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDefaultPaymentMethod(Guid paymentMethodId)
    {
        DefaultPaymentMethodId = paymentMethodId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AdjustBalance(decimal amount)
    {
        Balance += amount;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - not implemented yet
    }
}

public class InvoiceLineItem : Entity
{
    public Guid InvoiceId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime? PeriodStart { get; private set; }
    public DateTime? PeriodEnd { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public decimal DiscountAmount { get; private set; }

    private InvoiceLineItem() { }

    public static InvoiceLineItem Create(
        Guid invoiceId,
        string description,
        int quantity,
        decimal unitPrice,
        DateTime? periodStart = null,
        DateTime? periodEnd = null,
        decimal discountPercent = 0)
    {
        var amount = quantity * unitPrice;
        var discountAmount = amount * (discountPercent / 100);

        return new InvoiceLineItem
        {
            InvoiceId = invoiceId,
            Description = description,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Amount = amount - discountAmount,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            DiscountPercent = discountPercent,
            DiscountAmount = discountAmount
        };
    }
}
