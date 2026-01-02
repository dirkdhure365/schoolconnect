using SchoolConnect.Curriculum.Domain.Entities;
using SchoolConnect.Curriculum.Domain.Enums;
using FluentAssertions;

namespace SchoolConnect.Curriculum.Domain.Tests.Entities;

public class SubjectEntityTests
{
    [Fact]
    public void Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var subject = new SubjectEntity();

        // Assert
        subject.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void AddApplicableGrade_ShouldAddGradeToCollection()
    {
        // Arrange
        var subject = new SubjectEntity();

        // Act
        subject.AddApplicableGrade(1);
        subject.AddApplicableGrade(2);

        // Assert
        subject.ApplicableGrades.Should().HaveCount(2);
        subject.ApplicableGrades.Should().Contain(new[] { 1, 2 });
    }

    [Fact]
    public void Components_ShouldSupportCompositeSubjects()
    {
        // Arrange
        var parentSubject = new SubjectEntity { Name = "Language", Code = "LANG" };
        var component1 = new SubjectEntity { Name = "Reading", Code = "READ" };
        var component2 = new SubjectEntity { Name = "Writing", Code = "WRITE" };

        // Act
        parentSubject.AddComponent(component1);
        parentSubject.AddComponent(component2);

        // Assert
        parentSubject.Components.Should().NotBeNull();
        parentSubject.Components.Should().HaveCount(2);
        parentSubject.Components.Should().Contain(new[] { component1, component2 });
    }

    [Fact]
    public void AssessmentPolicy_ShouldBeNullable()
    {
        // Arrange & Act
        var subject = new SubjectEntity();

        // Assert
        subject.AssessmentPolicy.Should().BeNull();

        // Act - Set assessment policy
        subject.AssessmentPolicy = new AssessmentPolicyEntity
        {
            SubjectId = subject.Id,
            Description = "Test Policy"
        };

        // Assert
        subject.AssessmentPolicy.Should().NotBeNull();
    }

    [Fact]
    public void AddTopic_ShouldAddToTopicsCollection()
    {
        // Arrange
        var subject = new SubjectEntity();
        var topic = new TopicEntity { Name = "Algebra", SubjectId = subject.Id };

        // Act
        subject.AddTopic(topic);

        // Assert
        subject.Topics.Should().ContainSingle();
        subject.Topics.Should().Contain(topic);
    }

    [Fact]
    public void AddSkill_ShouldAddToSkillsCollection()
    {
        // Arrange
        var subject = new SubjectEntity();
        var skill = new SkillEntity 
        { 
            Name = "Problem Solving",
            Category = SkillCategory.ProblemSolving
        };

        // Act
        subject.AddSkill(skill);

        // Assert
        subject.Skills.Should().ContainSingle();
        subject.Skills.Should().Contain(skill);
    }

    [Fact]
    public void Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var subject = new SubjectEntity
        {
            Name = "Mathematics",
            Code = "MATH101",
            Type = SubjectType.Core,
            IsCompulsory = true,
            Description = "Core Mathematics"
        };

        // Assert
        subject.Name.Should().Be("Mathematics");
        subject.Code.Should().Be("MATH101");
        subject.Type.Should().Be(SubjectType.Core);
        subject.IsCompulsory.Should().BeTrue();
        subject.Description.Should().Be("Core Mathematics");
    }

    [Fact]
    public void Components_ShouldReturnNull_WhenNoComponents()
    {
        // Arrange & Act
        var subject = new SubjectEntity();

        // Assert
        subject.Components.Should().BeNull();
    }

    [Fact]
    public void AddApplicablePhase_ShouldAddPhaseIdToCollection()
    {
        // Arrange
        var subject = new SubjectEntity();
        var phaseId = Guid.NewGuid();

        // Act
        subject.AddApplicablePhase(phaseId);

        // Assert
        subject.ApplicablePhaseIds.Should().ContainSingle();
        subject.ApplicablePhaseIds.Should().Contain(phaseId);
    }

    [Fact]
    public void AddAim_ShouldAddToAimsCollection()
    {
        // Arrange
        var subject = new SubjectEntity();

        // Act
        subject.AddAim("Develop critical thinking");
        subject.AddAim("Master key concepts");

        // Assert
        subject.Aims.Should().HaveCount(2);
        subject.Aims.Should().Contain("Develop critical thinking");
        subject.Aims.Should().Contain("Master key concepts");
    }
}
