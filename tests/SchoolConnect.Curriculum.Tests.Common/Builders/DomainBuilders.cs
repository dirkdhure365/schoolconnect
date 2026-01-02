using SchoolConnect.Curriculum.Domain.Entities;
using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Tests.Common.Builders;

public class TopicBuilder
{
    private readonly TopicEntity _topic;

    public TopicBuilder()
    {
        _topic = new TopicEntity
        {
            Name = "Test Topic",
            Code = "TST-T01"
        };
    }

    public TopicBuilder WithName(string name)
    {
        _topic.Name = name;
        return this;
    }

    public TopicBuilder WithCode(string code)
    {
        _topic.Code = code;
        return this;
    }

    public TopicBuilder WithSubject(Guid subjectId)
    {
        _topic.SubjectId = subjectId;
        return this;
    }

    public TopicBuilder WithGrade(int grade)
    {
        _topic.AddApplicableGrade(grade);
        return this;
    }

    public TopicBuilder WithRecommendedHours(int hours)
    {
        _topic.RecommendedHours = hours;
        return this;
    }

    public TopicBuilder WithContentWeighting(decimal weighting)
    {
        _topic.ContentWeighting = weighting;
        return this;
    }

    public TopicEntity Build()
    {
        return _topic;
    }
}

public class EducationalPhaseBuilder
{
    private readonly EducationalPhaseEntity _phase;

    public EducationalPhaseBuilder()
    {
        _phase = new EducationalPhaseEntity
        {
            Name = "Test Phase",
            StartGrade = 1,
            EndGrade = 3
        };
    }

    public EducationalPhaseBuilder WithName(string name)
    {
        _phase.Name = name;
        return this;
    }

    public EducationalPhaseBuilder WithGrades(int start, int end)
    {
        _phase.StartGrade = start;
        _phase.EndGrade = end;
        return this;
    }

    public EducationalPhaseBuilder WithDescription(string description)
    {
        _phase.Description = description;
        return this;
    }

    public EducationalPhaseBuilder WithTeachingHours(int min, int max)
    {
        _phase.MinimumTeachingHours = min;
        _phase.MaximumTeachingHours = max;
        return this;
    }

    public EducationalPhaseEntity Build()
    {
        return _phase;
    }
}

public class AssessmentPolicyBuilder
{
    private readonly AssessmentPolicyEntity _policy;

    public AssessmentPolicyBuilder()
    {
        _policy = new AssessmentPolicyEntity();
    }

    public AssessmentPolicyBuilder ForSubject(Guid subjectId)
    {
        _policy.SubjectId = subjectId;
        return this;
    }

    public AssessmentPolicyBuilder WithDescription(string description)
    {
        _policy.Description = description;
        return this;
    }

    public AssessmentPolicyBuilder WithComponent(string name, AssessmentType type, decimal weight, int marks)
    {
        var component = new AssessmentComponentEntity
        {
            Name = name,
            Type = type,
            Weight = weight,
            TotalMarks = marks
        };
        _policy.AddComponent(component);
        return this;
    }

    public AssessmentPolicyBuilder WithAchievementScale(string rating, string description, decimal min, decimal max)
    {
        var scale = new AchievementScaleEntity
        {
            Rating = rating,
            Description = description,
            MinPercentage = min,
            MaxPercentage = max
        };
        _policy.AddAchievementScale(scale);
        return this;
    }

    public AssessmentPolicyEntity Build()
    {
        return _policy;
    }
}
