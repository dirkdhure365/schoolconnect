using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class ParentStudent : Entity
{
    public Guid ParentUserId { get; private set; }
    public Guid StudentId { get; private set; }
    public ParentRelationship Relationship { get; private set; }
    public bool IsPrimaryContact { get; private set; }
    public bool HasCustody { get; private set; }
    public bool CanPickUp { get; private set; }
    public bool ReceivesReports { get; private set; }
    public bool ReceivesBilling { get; private set; }

    private ParentStudent() { }

    public static ParentStudent Create(
        Guid parentUserId,
        Guid studentId,
        ParentRelationship relationship,
        bool isPrimaryContact = false,
        bool hasCustody = true,
        bool canPickUp = true,
        bool receivesReports = true,
        bool receivesBilling = false)
    {
        return new ParentStudent
        {
            ParentUserId = parentUserId,
            StudentId = studentId,
            Relationship = relationship,
            IsPrimaryContact = isPrimaryContact,
            HasCustody = hasCustody,
            CanPickUp = canPickUp,
            ReceivesReports = receivesReports,
            ReceivesBilling = receivesBilling
        };
    }

    public void Update(
        bool? isPrimaryContact = null,
        bool? hasCustody = null,
        bool? canPickUp = null,
        bool? receivesReports = null,
        bool? receivesBilling = null)
    {
        if (isPrimaryContact.HasValue) IsPrimaryContact = isPrimaryContact.Value;
        if (hasCustody.HasValue) HasCustody = hasCustody.Value;
        if (canPickUp.HasValue) CanPickUp = canPickUp.Value;
        if (receivesReports.HasValue) ReceivesReports = receivesReports.Value;
        if (receivesBilling.HasValue) ReceivesBilling = receivesBilling.Value;
        UpdatedAt = DateTime.UtcNow;
    }
}
