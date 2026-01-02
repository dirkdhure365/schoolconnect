using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Events;
using SchoolConnect.Institution.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.Entities;

public class StaffCentreAssignment : Entity
{
    public Guid StaffMemberId { get; private set; }
    public Guid CentreId { get; private set; }
    public bool IsPrimary { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public Guid AssignedBy { get; private set; }

    private StaffCentreAssignment() { }

    public static StaffCentreAssignment Create(
        Guid staffMemberId,
        Guid centreId,
        Guid assignedBy,
        DateTime startDate,
        bool isPrimary = false
    )
    {
        var assignment = new StaffCentreAssignment
        {
            StaffMemberId = staffMemberId,
            CentreId = centreId,
            AssignedBy = assignedBy,
            StartDate = startDate,
            IsPrimary = isPrimary
        };

        return assignment;
    }

    public void SetAsPrimary()
    {
        IsPrimary = true;
        MarkAsUpdated();
    }

    public void Remove(DateTime endDate)
    {
        EndDate = endDate;
        MarkAsUpdated();
    }
}
