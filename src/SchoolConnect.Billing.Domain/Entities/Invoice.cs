using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Billing.Domain.Enums;
using SchoolConnect.Billing.Domain.Events;

namespace SchoolConnect.Billing.Domain.Entities;

public class Invoice : AggregateRoot
{
    public string InvoiceNumber { get; private set; } = string.Empty;
    public Guid BillingAccountId { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public Guid InstituteId { get; private set; }
    public InvoiceStatus Status { get; private set; }
    public DateTime IssueDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? PaidDate { get; private set; }
    public List<InvoiceLineItem> LineItems { get; private set; } = new();
    public decimal Subtotal { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal TaxRate { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal Total { get; private set; }
    public decimal AmountPaid { get; private set; }
    public decimal AmountDue { get; private set; }
    public string Currency { get; private set; } = "USD";
    public string? Notes { get; private set; }

    private Invoice() { }

    public static Invoice Create(
        Guid billingAccountId,
        Guid subscriptionId,
        Guid instituteId,
        DateTime issueDate,
        DateTime dueDate,
        string currency,
        decimal taxRate = 0,
        string? notes = null)
    {
        var invoice = new Invoice
        {
            InvoiceNumber = GenerateInvoiceNumber(),
            BillingAccountId = billingAccountId,
            SubscriptionId = subscriptionId,
            InstituteId = instituteId,
            Status = InvoiceStatus.Draft,
            IssueDate = issueDate,
            DueDate = dueDate,
            Currency = currency,
            TaxRate = taxRate,
            Notes = notes,
            LineItems = new List<InvoiceLineItem>()
        };

        invoice.AddDomainEvent(new InvoiceCreatedEvent
        {
            AggregateId = invoice.Id,
            EventType = nameof(InvoiceCreatedEvent),
            InvoiceNumber = invoice.InvoiceNumber,
            InstituteId = instituteId
        });

        return invoice;
    }

    public void AddLineItem(InvoiceLineItem lineItem)
    {
        LineItems.Add(lineItem);
        RecalculateTotals();
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveLineItem(Guid lineItemId)
    {
        var item = LineItems.FirstOrDefault(i => i.Id == lineItemId);
        if (item != null)
        {
            LineItems.Remove(item);
            RecalculateTotals();
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Send()
    {
        Status = InvoiceStatus.Sent;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new InvoiceSentEvent
        {
            AggregateId = Id,
            EventType = nameof(InvoiceSentEvent),
            InvoiceNumber = InvoiceNumber,
            InstituteId = InstituteId
        });
    }

    public void MarkAsPaid(decimal amount, DateTime paidDate)
    {
        AmountPaid += amount;
        if (AmountPaid >= Total)
        {
            Status = InvoiceStatus.Paid;
            PaidDate = paidDate;
            AmountDue = 0;

            AddDomainEvent(new InvoicePaidEvent
            {
                AggregateId = Id,
                EventType = nameof(InvoicePaidEvent),
                InvoiceNumber = InvoiceNumber,
                InstituteId = InstituteId,
                Amount = Total
            });
        }
        else
        {
            Status = InvoiceStatus.PartiallyPaid;
            AmountDue = Total - AmountPaid;
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsOverdue()
    {
        if (Status == InvoiceStatus.Sent && DateTime.UtcNow > DueDate)
        {
            Status = InvoiceStatus.Overdue;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new InvoiceOverdueEvent
            {
                AggregateId = Id,
                EventType = nameof(InvoiceOverdueEvent),
                InvoiceNumber = InvoiceNumber,
                InstituteId = InstituteId
            });
        }
    }

    public void Cancel()
    {
        Status = InvoiceStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new InvoiceCancelledEvent
        {
            AggregateId = Id,
            EventType = nameof(InvoiceCancelledEvent),
            InvoiceNumber = InvoiceNumber,
            InstituteId = InstituteId
        });
    }

    private void RecalculateTotals()
    {
        Subtotal = LineItems.Sum(i => i.Amount);
        TaxAmount = Subtotal * (TaxRate / 100);
        Total = Subtotal + TaxAmount - DiscountAmount;
        AmountDue = Total - AmountPaid;
    }

    private static string GenerateInvoiceNumber()
    {
        return $"INV-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - not implemented yet
    }
}
