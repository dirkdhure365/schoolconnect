using SchoolConnect.Curriculum.Domain.Entities;
using SchoolConnect.Curriculum.Domain.Enums;
using FluentAssertions;

namespace SchoolConnect.Curriculum.Domain.Tests.Entities;

public class GradeCurriculumEntityTests
{
    [Fact]
    public void Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var gradeCurriculum = new GradeCurriculumEntity();

        // Assert
        gradeCurriculum.Id.Should().NotBe(Guid.Empty);
        gradeCurriculum.TermPlans.Should().BeEmpty();
    }

    [Fact]
    public void AddTermPlan_ShouldAddToCollection()
    {
        // Arrange
        var gradeCurriculum = new GradeCurriculumEntity();
        var termPlan = new TermPlanEntity
        {
            Term = SchoolTerm.Term1,
            WeeksInTerm = 10
        };

        // Act
        gradeCurriculum.AddTermPlan(termPlan);

        // Assert
        gradeCurriculum.TermPlans.Should().ContainSingle();
        gradeCurriculum.TermPlans.Should().Contain(termPlan);
    }

    [Fact]
    public void Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var subjectId = Guid.NewGuid();
        var gradeCurriculum = new GradeCurriculumEntity
        {
            SubjectId = subjectId,
            Grade = 10,
            Year = 2024,
            WeeklyTeachingHours = 4
        };

        // Assert
        gradeCurriculum.SubjectId.Should().Be(subjectId);
        gradeCurriculum.Grade.Should().Be(10);
        gradeCurriculum.Year.Should().Be(2024);
        gradeCurriculum.WeeklyTeachingHours.Should().Be(4);
    }

    [Fact]
    public void AssessmentRequirements_ShouldBeNullable()
    {
        // Arrange & Act
        var gradeCurriculum = new GradeCurriculumEntity();

        // Assert
        gradeCurriculum.AssessmentRequirements.Should().BeNull();

        // Act - Set requirements
        gradeCurriculum.AssessmentRequirements = new GradeAssessmentRequirementsEntity
        {
            Grade = 10,
            MinimumFormalAssessments = 7
        };

        // Assert
        gradeCurriculum.AssessmentRequirements.Should().NotBeNull();
    }

    [Fact]
    public void AddTermPlan_ShouldSupportMultipleTerms()
    {
        // Arrange
        var gradeCurriculum = new GradeCurriculumEntity();
        var term1 = new TermPlanEntity { Term = SchoolTerm.Term1, WeeksInTerm = 10 };
        var term2 = new TermPlanEntity { Term = SchoolTerm.Term2, WeeksInTerm = 10 };
        var term3 = new TermPlanEntity { Term = SchoolTerm.Term3, WeeksInTerm = 10 };
        var term4 = new TermPlanEntity { Term = SchoolTerm.Term4, WeeksInTerm = 10 };

        // Act
        gradeCurriculum.AddTermPlan(term1);
        gradeCurriculum.AddTermPlan(term2);
        gradeCurriculum.AddTermPlan(term3);
        gradeCurriculum.AddTermPlan(term4);

        // Assert
        gradeCurriculum.TermPlans.Should().HaveCount(4);
    }
}

public class TermPlanEntityTests
{
    [Fact]
    public void Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var termPlan = new TermPlanEntity();

        // Assert
        termPlan.Id.Should().NotBe(Guid.Empty);
        termPlan.Topics.Should().BeEmpty();
        termPlan.Assessments.Should().BeEmpty();
    }

    [Fact]
    public void AddTopic_ShouldAddToCollection()
    {
        // Arrange
        var termPlan = new TermPlanEntity();
        var termTopic = new TermTopicEntity
        {
            TopicId = Guid.NewGuid(),
            WeeksAllocated = 2,
            StartWeek = 1,
            EndWeek = 2
        };

        // Act
        termPlan.AddTopic(termTopic);

        // Assert
        termPlan.Topics.Should().ContainSingle();
        termPlan.Topics.Should().Contain(termTopic);
    }

    [Fact]
    public void AddAssessment_ShouldAddToCollection()
    {
        // Arrange
        var termPlan = new TermPlanEntity();
        var assessment = new FormalAssessmentTaskEntity
        {
            Name = "Test 1",
            Type = AssessmentType.Test,
            Term = SchoolTerm.Term1,
            TotalMarks = 50
        };

        // Act
        termPlan.AddAssessment(assessment);

        // Assert
        termPlan.Assessments.Should().ContainSingle();
        termPlan.Assessments.Should().Contain(assessment);
    }

    [Fact]
    public void Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var termPlan = new TermPlanEntity
        {
            Term = SchoolTerm.Term2,
            WeeksInTerm = 11
        };

        // Assert
        termPlan.Term.Should().Be(SchoolTerm.Term2);
        termPlan.WeeksInTerm.Should().Be(11);
    }
}
