using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.ValueObjects;

public class AudienceFilter : ValueObject
{
    public List<Guid>? CentreIds { get; private set; }
    public List<Guid>? CohortIds { get; private set; }
    public List<Guid>? ClassIds { get; private set; }
    public List<int>? GradeLevels { get; private set; }
    public List<string>? Roles { get; private set; }
    public List<Guid>? SpecificUserIds { get; private set; }

    private AudienceFilter() { }

    public static AudienceFilter Create(
        List<Guid>? centreIds = null,
        List<Guid>? cohortIds = null,
        List<Guid>? classIds = null,
        List<int>? gradeLevels = null,
        List<string>? roles = null,
        List<Guid>? specificUserIds = null)
    {
        return new AudienceFilter
        {
            CentreIds = centreIds,
            CohortIds = cohortIds,
            ClassIds = classIds,
            GradeLevels = gradeLevels,
            Roles = roles,
            SpecificUserIds = specificUserIds
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        if (CentreIds != null)
            foreach (var id in CentreIds.OrderBy(x => x))
                yield return id;
        
        if (CohortIds != null)
            foreach (var id in CohortIds.OrderBy(x => x))
                yield return id;
        
        if (ClassIds != null)
            foreach (var id in ClassIds.OrderBy(x => x))
                yield return id;
        
        if (GradeLevels != null)
            foreach (var level in GradeLevels.OrderBy(x => x))
                yield return level;
        
        if (Roles != null)
            foreach (var role in Roles.OrderBy(x => x))
                yield return role;
        
        if (SpecificUserIds != null)
            foreach (var id in SpecificUserIds.OrderBy(x => x))
                yield return id;
    }
}
