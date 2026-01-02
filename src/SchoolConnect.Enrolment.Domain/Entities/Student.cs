using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;
using SchoolConnect.Enrolment.Domain.Events;
using SchoolConnect.Enrolment.Domain.ValueObjects;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class Student : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid? UserId { get; private set; }
    public string StudentCode { get; private set; } = string.Empty;
    
    // Personal Info
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? MiddleName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }
    public string? Nationality { get; private set; }
    public string? IdNumber { get; private set; }
    public string? PassportNumber { get; private set; }
    public string? PhotoUrl { get; private set; }
    
    // Contact
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public Address? Address { get; private set; }
    
    // Medical & Emergency
    public MedicalInfo? MedicalInfo { get; private set; }
    public List<EmergencyContact> EmergencyContacts { get; private set; } = [];
    
    // Academic
    public PreviousSchoolInfo? PreviousSchool { get; private set; }
    public string? AdmissionNumber { get; private set; }
    public Guid? ApplicationId { get; private set; }
    
    // Status
    public StudentStatus Status { get; private set; }
    public DateTime EnrolledAt { get; private set; }
    public DateTime? GraduatedAt { get; private set; }
    public DateTime? WithdrawnAt { get; private set; }
    public string? WithdrawalReason { get; private set; }

    private Student() { }

    public static Student Create(
        Guid instituteId,
        string studentCode,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        Gender gender,
        string? middleName = null,
        string? nationality = null,
        string? idNumber = null,
        string? passportNumber = null,
        string? email = null,
        string? phoneNumber = null,
        Address? address = null,
        PreviousSchoolInfo? previousSchool = null,
        string? admissionNumber = null,
        Guid? applicationId = null,
        Guid? userId = null)
    {
        var student = new Student
        {
            InstituteId = instituteId,
            UserId = userId,
            StudentCode = studentCode,
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            Nationality = nationality,
            IdNumber = idNumber,
            PassportNumber = passportNumber,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            PreviousSchool = previousSchool,
            AdmissionNumber = admissionNumber,
            ApplicationId = applicationId,
            Status = StudentStatus.Active,
            EnrolledAt = DateTime.UtcNow
        };

        student.AddDomainEvent(new StudentCreatedEvent
        {
            AggregateId = student.Id,
            AggregateType = nameof(Student),
            InstituteId = instituteId,
            StudentCode = studentCode,
            FirstName = firstName,
            LastName = lastName
        });

        return student;
    }

    public void Update(
        string firstName,
        string lastName,
        string? middleName = null,
        string? nationality = null,
        string? idNumber = null,
        string? passportNumber = null,
        string? email = null,
        string? phoneNumber = null,
        Address? address = null)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Nationality = nationality;
        IdNumber = idNumber;
        PassportNumber = passportNumber;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new StudentUpdatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Student),
            StudentCode = StudentCode
        });
    }

    public void UpdatePhoto(string photoUrl)
    {
        PhotoUrl = photoUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateMedicalInfo(MedicalInfo medicalInfo)
    {
        MedicalInfo = medicalInfo;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddEmergencyContact(EmergencyContact contact)
    {
        EmergencyContacts.Add(contact);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveEmergencyContact(string name)
    {
        EmergencyContacts.RemoveAll(c => c.Name == name);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Transfer(string reason)
    {
        Status = StudentStatus.Transferred;
        WithdrawalReason = reason;
        WithdrawnAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Withdraw(string reason)
    {
        Status = StudentStatus.Withdrawn;
        WithdrawalReason = reason;
        WithdrawnAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new StudentWithdrawnEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Student),
            StudentId = Id,
            Reason = reason,
            WithdrawnAt = WithdrawnAt.Value
        });
    }

    public void Graduate()
    {
        Status = StudentStatus.Graduated;
        GraduatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new StudentGraduatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Student),
            StudentId = Id,
            StreamId = Guid.Empty, // Would need to be passed in
            GraduatedAt = GraduatedAt.Value
        });
    }

    public void Suspend()
    {
        Status = StudentStatus.Suspended;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = StudentStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
