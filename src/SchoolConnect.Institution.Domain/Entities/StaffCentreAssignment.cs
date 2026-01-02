using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Events;

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
        bool isPrimary = false,
        DateTime? startDate = null)
    {
        var assignment = new StaffCentreAssignment
        {
            StaffMemberId = staffMemberId,
            CentreId = centreId,
            IsPrimary = isPrimary,
            StartDate = startDate ?? DateTime.UtcNow,
            AssignedBy = assignedBy
        };

        return assignment;
    }

    public void End(DateTime? endDate = null)
    {
        EndDate = endDate ?? DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetAsPrimary()
    {
        IsPrimary = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
