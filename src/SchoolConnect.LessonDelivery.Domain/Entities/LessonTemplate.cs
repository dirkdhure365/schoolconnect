using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class LessonTemplate : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid? CentreId { get; private set; }
    public Guid? SubjectId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public TemplateStructure Structure { get; private set; } = new();
    public new Guid CreatedBy { get; private set; }
    public bool IsActive { get; private set; }

    private LessonTemplate() { }

    public static LessonTemplate Create(
        Guid instituteId,
        string name,
        TemplateStructure structure,
        Guid createdBy,
        Guid? centreId = null,
        Guid? subjectId = null,
        string? description = null)
    {
        return new LessonTemplate
        {
            InstituteId = instituteId,
            CentreId = centreId,
            SubjectId = subjectId,
            Name = name,
            Description = description,
            Structure = structure,
            CreatedBy = createdBy,
            IsActive = true
        };
    }

    public void Update(string name, TemplateStructure structure, string? description = null)
    {
        Name = name;
        Description = description;
        Structure = structure;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

public class TemplateStructure
{
    public List<TemplateSection> Sections { get; set; } = [];
    public List<string> RequiredFields { get; set; } = [];
    public int DefaultDurationMinutes { get; set; }
}

public class TemplateSection
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public bool IsRequired { get; set; }
}
