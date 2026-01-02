using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class StudentBillingAccount : AggregateRoot
{
    public Guid StudentId { get; private set; }
    public Guid InstituteId { get; private set; }
    public string AccountNumber { get; private set; } = string.Empty;
    public decimal Balance { get; private set; }
    public decimal CreditLimit { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    public BillingAccountStatus Status { get; private set; }
    public Guid? PrimaryPayerUserId { get; private set; }

    private StudentBillingAccount() { }

    public static StudentBillingAccount Create(
        Guid studentId,
        Guid instituteId,
        string accountNumber,
        string currency,
        decimal creditLimit = 0,
        Guid? primaryPayerUserId = null)
    {
        return new StudentBillingAccount
        {
            StudentId = studentId,
            InstituteId = instituteId,
            AccountNumber = accountNumber,
            Balance = 0,
            CreditLimit = creditLimit,
            Currency = currency,
            Status = BillingAccountStatus.Active,
            PrimaryPayerUserId = primaryPayerUserId
        };
    }

    public void UpdateBalance(decimal amount)
    {
        Balance += amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateCreditLimit(decimal creditLimit)
    {
        CreditLimit = creditLimit;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePrimaryPayer(Guid? primaryPayerUserId)
    {
        PrimaryPayerUserId = primaryPayerUserId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Suspend()
    {
        Status = BillingAccountStatus.Suspended;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = BillingAccountStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        Status = BillingAccountStatus.Closed;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
