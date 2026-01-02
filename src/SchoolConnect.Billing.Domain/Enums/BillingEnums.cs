namespace SchoolConnect.Billing.Domain.Enums;

public enum InvoiceStatus
{
    Draft,
    Pending,
    Sent,
    Paid,
    PartiallyPaid,
    Overdue,
    Cancelled,
    Refunded
}

public enum PaymentStatus
{
    Pending,
    Processing,
    Completed,
    Failed,
    Refunded,
    PartiallyRefunded
}

public enum PaymentMethodType
{
    Card,
    BankAccount,
    MobileMoney
}

public enum PaymentMethodEnum
{
    Card,
    BankTransfer,
    MobileMoney,
    Cash,
    Check
}

public enum TransactionType
{
    Charge,
    Refund,
    Credit,
    Adjustment,
    WriteOff
}

public enum CreditNoteStatus
{
    Issued,
    Applied,
    Expired
}
