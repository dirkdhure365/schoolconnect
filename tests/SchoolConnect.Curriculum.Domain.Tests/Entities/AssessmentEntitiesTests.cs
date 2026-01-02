using SchoolConnect.Curriculum.Domain.Entities;
using SchoolConnect.Curriculum.Domain.Enums;
using FluentAssertions;

namespace SchoolConnect.Curriculum.Domain.Tests.Entities;

public class AssessmentEntitiesTests
{
    [Fact]
    public void AssessmentPolicyEntity_Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var policy = new AssessmentPolicyEntity();

        // Assert
        policy.Id.Should().NotBe(Guid.Empty);
        policy.Components.Should().BeEmpty();
        policy.AchievementScales.Should().BeEmpty();
    }

    [Fact]
    public void AssessmentPolicyEntity_AddComponent_ShouldAddToCollection()
    {
        // Arrange
        var policy = new AssessmentPolicyEntity();
        var component = new AssessmentComponentEntity
        {
            Name = "Test",
            Type = AssessmentType.Test,
            Weight = 0.4m,
            TotalMarks = 100
        };

        // Act
        policy.AddComponent(component);

        // Assert
        policy.Components.Should().ContainSingle();
        policy.Components.Should().Contain(component);
    }

    [Fact]
    public void AssessmentComponentEntity_Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var component = new AssessmentComponentEntity
        {
            Name = "Mid-year Exam",
            Type = AssessmentType.WrittenExamination,
            Weight = 0.5m,
            DurationMinutes = 180,
            TotalMarks = 150,
            Description = "Comprehensive mid-year examination"
        };

        // Assert
        component.Id.Should().NotBe(Guid.Empty);
        component.Name.Should().Be("Mid-year Exam");
        component.Type.Should().Be(AssessmentType.WrittenExamination);
        component.Weight.Should().Be(0.5m);
        component.DurationMinutes.Should().Be(180);
        component.TotalMarks.Should().Be(150);
        component.Description.Should().Be("Comprehensive mid-year examination");
    }

    [Fact]
    public void FormalAssessmentTaskEntity_AddTopic_ShouldAddToCollection()
    {
        // Arrange
        var task = new FormalAssessmentTaskEntity();
        var topicId = Guid.NewGuid();

        // Act
        task.AddTopic(topicId);

        // Assert
        task.TopicIds.Should().ContainSingle();
        task.TopicIds.Should().Contain(topicId);
    }

    [Fact]
    public void GradeAssessmentRequirementsEntity_AddRequiredTask_ShouldAddToCollection()
    {
        // Arrange
        var requirements = new GradeAssessmentRequirementsEntity();
        var task = new FormalAssessmentTaskEntity
        {
            Name = "Test 1",
            Type = AssessmentType.Test,
            Term = SchoolTerm.Term1,
            TotalMarks = 50
        };

        // Act
        requirements.AddRequiredTask(task);

        // Assert
        requirements.RequiredTasks.Should().ContainSingle();
        requirements.RequiredTasks.Should().Contain(task);
    }

    [Fact]
    public void GradeAssessmentRequirementsEntity_Properties_ShouldCalculateCorrectly()
    {
        // Arrange & Act
        var requirements = new GradeAssessmentRequirementsEntity
        {
            Grade = 10,
            MinimumFormalAssessments = 7,
            SchoolBasedAssessmentWeight = 0.25m,
            FinalExaminationWeight = 0.75m
        };

        // Assert
        requirements.Grade.Should().Be(10);
        requirements.MinimumFormalAssessments.Should().Be(7);
        requirements.SchoolBasedAssessmentWeight.Should().Be(0.25m);
        requirements.FinalExaminationWeight.Should().Be(0.75m);
        (requirements.SchoolBasedAssessmentWeight + requirements.FinalExaminationWeight).Should().Be(1.0m);
    }

    [Fact]
    public void ProgrammeOfAssessmentEntity_AddFormalTask_ShouldAddToCollection()
    {
        // Arrange
        var programme = new ProgrammeOfAssessmentEntity();
        var task = new FormalAssessmentTaskEntity
        {
            Name = "Term 1 Test",
            Type = AssessmentType.Test,
            Term = SchoolTerm.Term1
        };

        // Act
        programme.AddFormalTask(task);

        // Assert
        programme.FormalTasks.Should().ContainSingle();
        programme.FormalTasks.Should().Contain(task);
    }

    [Fact]
    public void AchievementScaleEntity_ShouldHaveValidPercentages()
    {
        // Arrange & Act
        var scale = new AchievementScaleEntity
        {
            Rating = "7",
            Description = "Outstanding Achievement",
            MinPercentage = 80m,
            MaxPercentage = 100m
        };

        // Assert
        scale.Rating.Should().Be("7");
        scale.Description.Should().Be("Outstanding Achievement");
        scale.MinPercentage.Should().Be(80m);
        scale.MaxPercentage.Should().Be(100m);
        scale.MinPercentage.Should().BeLessThan(scale.MaxPercentage);
        scale.MinPercentage.Should().BeInRange(0m, 100m);
        scale.MaxPercentage.Should().BeInRange(0m, 100m);
    }

    [Fact]
    public void FormalAssessmentTaskEntity_Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var task = new FormalAssessmentTaskEntity
        {
            Name = "Assignment 1",
            Type = AssessmentType.Coursework,
            Term = SchoolTerm.Term2,
            TotalMarks = 50,
            Weight = 0.15m,
            DurationMinutes = 60
        };

        // Assert
        task.Id.Should().NotBe(Guid.Empty);
        task.Name.Should().Be("Assignment 1");
        task.Type.Should().Be(AssessmentType.Coursework);
        task.Term.Should().Be(SchoolTerm.Term2);
        task.TotalMarks.Should().Be(50);
        task.Weight.Should().Be(0.15m);
        task.DurationMinutes.Should().Be(60);
    }

    [Fact]
    public void AssessmentPolicyEntity_AddAchievementScale_ShouldAddToCollection()
    {
        // Arrange
        var policy = new AssessmentPolicyEntity();
        var scale1 = new AchievementScaleEntity { Rating = "7", MinPercentage = 80m, MaxPercentage = 100m };
        var scale2 = new AchievementScaleEntity { Rating = "6", MinPercentage = 70m, MaxPercentage = 79m };

        // Act
        policy.AddAchievementScale(scale1);
        policy.AddAchievementScale(scale2);

        // Assert
        policy.AchievementScales.Should().HaveCount(2);
        policy.AchievementScales.Should().Contain(new[] { scale1, scale2 });
    }
}
