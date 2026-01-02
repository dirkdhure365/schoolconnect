using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Billing.Domain.Enums;
using SchoolConnect.Billing.Domain.Events;
using SchoolConnect.Billing.Domain.ValueObjects;

namespace SchoolConnect.Billing.Domain.Entities;

public class Payment : AggregateRoot
{
    public Guid InvoiceId { get; private set; }
    public Guid BillingAccountId { get; private set; }
    public Guid? PaymentMethodId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public PaymentStatus Status { get; private set; }
    public PaymentMethodEnum PaymentMethod { get; private set; }
    public string? TransactionReference { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public DateTime? FailedAt { get; private set; }
    public string? FailureReason { get; private set; }
    public Dictionary<string, string> Metadata { get; private set; } = new();

    private Payment() { }

    public static Payment Create(
        Guid invoiceId,
        Guid billingAccountId,
        decimal amount,
        string currency,
        PaymentMethodEnum paymentMethod,
        Guid? paymentMethodId = null)
    {
        var payment = new Payment
        {
            InvoiceId = invoiceId,
            BillingAccountId = billingAccountId,
            PaymentMethodId = paymentMethodId,
            Amount = amount,
            Currency = currency,
            PaymentMethod = paymentMethod,
            Status = PaymentStatus.Pending,
            Metadata = new Dictionary<string, string>()
        };

        payment.AddDomainEvent(new PaymentInitiatedEvent
        {
            AggregateId = payment.Id,
            EventType = nameof(PaymentInitiatedEvent),
            InvoiceId = invoiceId,
            Amount = amount
        });

        return payment;
    }

    public void MarkAsProcessing()
    {
        Status = PaymentStatus.Processing;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted(string transactionReference)
    {
        Status = PaymentStatus.Completed;
        TransactionReference = transactionReference;
        ProcessedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new PaymentCompletedEvent
        {
            AggregateId = Id,
            EventType = nameof(PaymentCompletedEvent),
            InvoiceId = InvoiceId,
            Amount = Amount,
            TransactionReference = transactionReference
        });
    }

    public void MarkAsFailed(string reason)
    {
        Status = PaymentStatus.Failed;
        FailureReason = reason;
        FailedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new PaymentFailedEvent
        {
            AggregateId = Id,
            EventType = nameof(PaymentFailedEvent),
            InvoiceId = InvoiceId,
            Reason = reason
        });
    }

    public void Refund(decimal amount)
    {
        if (amount >= Amount)
        {
            Status = PaymentStatus.Refunded;
        }
        else
        {
            Status = PaymentStatus.PartiallyRefunded;
        }
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new PaymentRefundedEvent
        {
            AggregateId = Id,
            EventType = nameof(PaymentRefundedEvent),
            InvoiceId = InvoiceId,
            RefundAmount = amount
        });
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - not implemented yet
    }
}

public class PaymentMethod : AggregateRoot
{
    public Guid BillingAccountId { get; private set; }
    public PaymentMethodType Type { get; private set; }
    public bool IsDefault { get; private set; }
    public CardDetails? CardDetails { get; private set; }
    public BankDetails? BankDetails { get; private set; }
    public MobileMoneyDetails? MobileMoneyDetails { get; private set; }
    public string? ExternalId { get; private set; }
    public bool IsActive { get; private set; }

    private PaymentMethod() { }

    public static PaymentMethod CreateCard(
        Guid billingAccountId,
        CardDetails cardDetails,
        string? externalId = null,
        bool isDefault = false)
    {
        var method = new PaymentMethod
        {
            BillingAccountId = billingAccountId,
            Type = PaymentMethodType.Card,
            CardDetails = cardDetails,
            ExternalId = externalId,
            IsDefault = isDefault,
            IsActive = true
        };

        method.AddDomainEvent(new PaymentMethodAddedEvent
        {
            AggregateId = method.Id,
            EventType = nameof(PaymentMethodAddedEvent),
            BillingAccountId = billingAccountId,
            Type = PaymentMethodType.Card.ToString()
        });

        return method;
    }

    public static PaymentMethod CreateBankAccount(
        Guid billingAccountId,
        BankDetails bankDetails,
        string? externalId = null,
        bool isDefault = false)
    {
        var method = new PaymentMethod
        {
            BillingAccountId = billingAccountId,
            Type = PaymentMethodType.BankAccount,
            BankDetails = bankDetails,
            ExternalId = externalId,
            IsDefault = isDefault,
            IsActive = true
        };

        method.AddDomainEvent(new PaymentMethodAddedEvent
        {
            AggregateId = method.Id,
            EventType = nameof(PaymentMethodAddedEvent),
            BillingAccountId = billingAccountId,
            Type = PaymentMethodType.BankAccount.ToString()
        });

        return method;
    }

    public static PaymentMethod CreateMobileMoney(
        Guid billingAccountId,
        MobileMoneyDetails mobileMoneyDetails,
        string? externalId = null,
        bool isDefault = false)
    {
        var method = new PaymentMethod
        {
            BillingAccountId = billingAccountId,
            Type = PaymentMethodType.MobileMoney,
            MobileMoneyDetails = mobileMoneyDetails,
            ExternalId = externalId,
            IsDefault = isDefault,
            IsActive = true
        };

        method.AddDomainEvent(new PaymentMethodAddedEvent
        {
            AggregateId = method.Id,
            EventType = nameof(PaymentMethodAddedEvent),
            BillingAccountId = billingAccountId,
            Type = PaymentMethodType.MobileMoney.ToString()
        });

        return method;
    }

    public void SetAsDefault()
    {
        IsDefault = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new PaymentMethodRemovedEvent
        {
            AggregateId = Id,
            EventType = nameof(PaymentMethodRemovedEvent),
            BillingAccountId = BillingAccountId
        });
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - not implemented yet
    }
}

public class Transaction : Entity
{
    public Guid BillingAccountId { get; private set; }
    public Guid? InvoiceId { get; private set; }
    public Guid? PaymentId { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public decimal BalanceBefore { get; private set; }
    public decimal BalanceAfter { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string? Reference { get; private set; }
    public string? CreatedBy { get; private set; }

    private Transaction() { }

    public static Transaction Create(
        Guid billingAccountId,
        TransactionType type,
        decimal amount,
        string currency,
        decimal balanceBefore,
        string description,
        Guid? invoiceId = null,
        Guid? paymentId = null,
        string? reference = null,
        string? createdBy = null)
    {
        var balanceAfter = type switch
        {
            TransactionType.Charge => balanceBefore - amount,
            TransactionType.Credit or TransactionType.Refund => balanceBefore + amount,
            TransactionType.Adjustment => balanceBefore + amount,
            TransactionType.WriteOff => balanceBefore - amount,
            _ => balanceBefore
        };

        return new Transaction
        {
            BillingAccountId = billingAccountId,
            InvoiceId = invoiceId,
            PaymentId = paymentId,
            Type = type,
            Amount = amount,
            Currency = currency,
            BalanceBefore = balanceBefore,
            BalanceAfter = balanceAfter,
            Description = description,
            Reference = reference,
            CreatedBy = createdBy
        };
    }
}

public class CreditNote : AggregateRoot
{
    public Guid BillingAccountId { get; private set; }
    public Guid? InvoiceId { get; private set; }
    public string CreditNoteNumber { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public string Reason { get; private set; } = string.Empty;
    public CreditNoteStatus Status { get; private set; }
    public DateTime IssuedAt { get; private set; }
    public DateTime? AppliedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    public Guid? AppliedToInvoiceId { get; private set; }
    public string? CreatedBy { get; private set; }

    private CreditNote() { }

    public static CreditNote Create(
        Guid billingAccountId,
        decimal amount,
        string currency,
        string reason,
        Guid? invoiceId = null,
        DateTime? expiresAt = null,
        string? createdBy = null)
    {
        var creditNote = new CreditNote
        {
            BillingAccountId = billingAccountId,
            InvoiceId = invoiceId,
            CreditNoteNumber = GenerateCreditNoteNumber(),
            Amount = amount,
            Currency = currency,
            Reason = reason,
            Status = CreditNoteStatus.Issued,
            IssuedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt,
            CreatedBy = createdBy
        };

        creditNote.AddDomainEvent(new CreditNoteIssuedEvent
        {
            AggregateId = creditNote.Id,
            EventType = nameof(CreditNoteIssuedEvent),
            CreditNoteNumber = creditNote.CreditNoteNumber,
            Amount = amount
        });

        return creditNote;
    }

    public void Apply(Guid invoiceId)
    {
        Status = CreditNoteStatus.Applied;
        AppliedAt = DateTime.UtcNow;
        AppliedToInvoiceId = invoiceId;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new CreditNoteAppliedEvent
        {
            AggregateId = Id,
            EventType = nameof(CreditNoteAppliedEvent),
            CreditNoteNumber = CreditNoteNumber,
            InvoiceId = invoiceId
        });
    }

    public void Expire()
    {
        Status = CreditNoteStatus.Expired;
        UpdatedAt = DateTime.UtcNow;
    }

    private static string GenerateCreditNoteNumber()
    {
        return $"CN-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - not implemented yet
    }
}
