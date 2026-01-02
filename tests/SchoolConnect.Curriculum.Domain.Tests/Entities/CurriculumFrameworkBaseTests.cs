using SchoolConnect.Curriculum.Domain.Entities;
using SchoolConnect.Curriculum.Domain.Abstractions;
using FluentAssertions;

namespace SchoolConnect.Curriculum.Domain.Tests.Entities;

public class CurriculumFrameworkBaseTests
{
    private class TestCurriculumFramework : CurriculumFrameworkBase
    {
        public TestCurriculumFramework()
        {
            Name = "Test Framework";
            Code = "TEST";
            Country = "Test Country";
            ExaminationBoard = "Test Board";
        }
    }

    [Fact]
    public void Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var framework = new TestCurriculumFramework();

        // Assert
        framework.Id.Should().NotBe(Guid.Empty);
        framework.Name.Should().Be("Test Framework");
        framework.Code.Should().Be("TEST");
        framework.Country.Should().Be("Test Country");
        framework.ExaminationBoard.Should().Be("Test Board");
        framework.Phases.Should().BeEmpty();
        framework.Subjects.Should().BeEmpty();
        framework.Principles.Should().BeEmpty();
        framework.GeneralAims.Should().BeEmpty();
    }

    [Fact]
    public void AddPhase_ShouldAddPhaseToCollection()
    {
        // Arrange
        var framework = new TestCurriculumFramework();
        var phase = new EducationalPhaseEntity
        {
            Name = "Foundation Phase",
            StartGrade = 1,
            EndGrade = 3
        };

        // Act
        framework.AddPhase(phase);

        // Assert
        framework.Phases.Should().ContainSingle();
        framework.Phases.Should().Contain(phase);
    }

    [Fact]
    public void AddSubject_ShouldAddSubjectToCollection()
    {
        // Arrange
        var framework = new TestCurriculumFramework();
        var subject = new SubjectEntity
        {
            Name = "Mathematics",
            Code = "MATH"
        };

        // Act
        framework.AddSubject(subject);

        // Assert
        framework.Subjects.Should().ContainSingle();
        framework.Subjects.Should().Contain(subject);
    }

    [Fact]
    public void GetSubjectsByPhase_ShouldReturnMatchingSubjects()
    {
        // Arrange
        var framework = new TestCurriculumFramework();
        var phase = new EducationalPhaseEntity { Name = "Foundation Phase" };
        framework.AddPhase(phase);

        var subject1 = new SubjectEntity { Name = "Math", Code = "MATH" };
        subject1.AddApplicablePhase(phase.Id);
        
        var subject2 = new SubjectEntity { Name = "Science", Code = "SCI" };
        subject2.AddApplicablePhase(Guid.NewGuid()); // Different phase

        framework.AddSubject(subject1);
        framework.AddSubject(subject2);

        // Act
        var results = framework.GetSubjectsByPhase(phase.Id);

        // Assert
        results.Should().ContainSingle();
        results.Should().Contain(subject1);
        results.Should().NotContain(subject2);
    }

    [Fact]
    public void GetSubjectsByGrade_ShouldReturnMatchingSubjects()
    {
        // Arrange
        var framework = new TestCurriculumFramework();
        
        var subject1 = new SubjectEntity { Name = "Math", Code = "MATH" };
        subject1.AddApplicableGrade(1);
        subject1.AddApplicableGrade(2);
        
        var subject2 = new SubjectEntity { Name = "Science", Code = "SCI" };
        subject2.AddApplicableGrade(3);

        framework.AddSubject(subject1);
        framework.AddSubject(subject2);

        // Act
        var results = framework.GetSubjectsByGrade(1);

        // Assert
        results.Should().ContainSingle();
        results.Should().Contain(subject1);
        results.Should().NotContain(subject2);
    }

    [Fact]
    public void Principles_ShouldBeInitialized()
    {
        // Arrange
        var framework = new TestCurriculumFramework();

        // Act
        framework.AddPrinciple("Principle 1");
        framework.AddPrinciple("Principle 2");

        // Assert
        framework.Principles.Should().HaveCount(2);
        framework.Principles.Should().Contain("Principle 1");
        framework.Principles.Should().Contain("Principle 2");
    }

    [Fact]
    public void GeneralAims_ShouldBeInitialized()
    {
        // Arrange
        var framework = new TestCurriculumFramework();

        // Act
        framework.AddGeneralAim("Aim 1");
        framework.AddGeneralAim("Aim 2");

        // Assert
        framework.GeneralAims.Should().HaveCount(2);
        framework.GeneralAims.Should().Contain("Aim 1");
        framework.GeneralAims.Should().Contain("Aim 2");
    }

    [Fact]
    public void GetSubjectsByPhase_ShouldReturnEmpty_WhenPhaseNotFound()
    {
        // Arrange
        var framework = new TestCurriculumFramework();
        var nonExistentPhaseId = Guid.NewGuid();

        // Act
        var results = framework.GetSubjectsByPhase(nonExistentPhaseId);

        // Assert
        results.Should().BeEmpty();
    }

    [Fact]
    public void GetSubjectsByGrade_ShouldReturnEmpty_WhenNoSubjectsForGrade()
    {
        // Arrange
        var framework = new TestCurriculumFramework();

        // Act
        var results = framework.GetSubjectsByGrade(5);

        // Assert
        results.Should().BeEmpty();
    }
}
