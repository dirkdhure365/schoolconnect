using SchoolConnect.Curriculum.Domain.Abstractions;
using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Entities;

/// <summary>
/// Entity implementation of IGradeCurriculum.
/// </summary>
public class GradeCurriculumEntity : IGradeCurriculum
{
    public Guid Id { get; set; }
    public Guid SubjectId { get; set; }
    public int Grade { get; set; }
    public int? Year { get; set; }
    public int WeeklyTeachingHours { get; set; }
    public IGradeAssessmentRequirements? AssessmentRequirements { get; set; }

    private readonly List<ITermPlan> _termPlans = new();
    public IReadOnlyCollection<ITermPlan> TermPlans => _termPlans.AsReadOnly();

    public GradeCurriculumEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddTermPlan(ITermPlan termPlan) => _termPlans.Add(termPlan);
}

/// <summary>
/// Entity implementation of ITermPlan.
/// </summary>
public class TermPlanEntity : ITermPlan
{
    public Guid Id { get; set; }
    public SchoolTerm Term { get; set; }
    public int WeeksInTerm { get; set; }

    private readonly List<ITermTopic> _topics = new();
    private readonly List<IFormalAssessmentTask> _assessments = new();

    public IReadOnlyCollection<ITermTopic> Topics => _topics.AsReadOnly();
    public IReadOnlyCollection<IFormalAssessmentTask> Assessments => _assessments.AsReadOnly();

    public TermPlanEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddTopic(ITermTopic topic) => _topics.Add(topic);
    public void AddAssessment(IFormalAssessmentTask assessment) => _assessments.Add(assessment);
}

/// <summary>
/// Entity implementation of ITermTopic.
/// </summary>
public class TermTopicEntity : ITermTopic
{
    public Guid TopicId { get; set; }
    public int WeeksAllocated { get; set; }
    public int StartWeek { get; set; }
    public int EndWeek { get; set; }
}
