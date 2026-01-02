using SchoolConnect.Curriculum.Domain.Entities;
using SchoolConnect.Curriculum.Domain.Enums;
using SchoolConnect.Curriculum.Domain.Abstractions;

namespace SchoolConnect.Curriculum.Tests.Common.Builders;

public class SubjectBuilder
{
    private readonly SubjectEntity _subject;

    public SubjectBuilder()
    {
        _subject = new SubjectEntity
        {
            Name = "Test Subject",
            Code = "TST101",
            Type = SubjectType.Core,
            IsCompulsory = false
        };
    }

    public SubjectBuilder WithName(string name)
    {
        _subject.Name = name;
        return this;
    }

    public SubjectBuilder WithCode(string code)
    {
        _subject.Code = code;
        return this;
    }

    public SubjectBuilder WithType(SubjectType type)
    {
        _subject.Type = type;
        return this;
    }

    public SubjectBuilder IsCompulsory(bool compulsory)
    {
        _subject.IsCompulsory = compulsory;
        return this;
    }

    public SubjectBuilder WithGrades(params int[] grades)
    {
        foreach (var grade in grades)
        {
            _subject.AddApplicableGrade(grade);
        }
        return this;
    }

    public SubjectBuilder WithTopics(params ITopic[] topics)
    {
        foreach (var topic in topics)
        {
            _subject.AddTopic(topic);
        }
        return this;
    }

    public SubjectBuilder WithPhase(Guid phaseId)
    {
        _subject.AddApplicablePhase(phaseId);
        return this;
    }

    public SubjectEntity Build()
    {
        return _subject;
    }
}
