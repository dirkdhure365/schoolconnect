using SchoolConnect.Curriculum.Domain.Entities;
using FluentAssertions;

namespace SchoolConnect.Curriculum.Domain.Tests.Entities;

public class EducationalPhaseEntityTests
{
    [Fact]
    public void Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var phase = new EducationalPhaseEntity();

        // Assert
        phase.Id.Should().NotBe(Guid.Empty);
    }

    [Theory]
    [InlineData(1, 3, 2, true)]
    [InlineData(1, 3, 5, false)]
    [InlineData(1, 3, 1, true)]
    [InlineData(1, 3, 3, true)]
    [InlineData(4, 6, 3, false)]
    [InlineData(4, 6, 7, false)]
    public void ContainsGrade_ShouldReturnCorrectResult(int start, int end, int grade, bool expected)
    {
        // Arrange
        var phase = new EducationalPhaseEntity
        {
            StartGrade = start,
            EndGrade = end
        };

        // Act
        var result = phase.ContainsGrade(grade);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void AddGradeCurriculum_ShouldAddToCollection()
    {
        // Arrange
        var phase = new EducationalPhaseEntity();
        var gradeCurriculum = new GradeCurriculumEntity { Grade = 1 };

        // Act
        phase.AddGradeCurriculum(gradeCurriculum);

        // Assert
        phase.GradeCurricula.Should().ContainSingle();
        phase.GradeCurricula.Should().Contain(gradeCurriculum);
    }

    [Fact]
    public void GradeCurricula_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var phase = new EducationalPhaseEntity();

        // Assert
        phase.GradeCurricula.Should().NotBeNull();
        phase.GradeCurricula.Should().BeEmpty();
    }

    [Fact]
    public void Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var phase = new EducationalPhaseEntity
        {
            Name = "Foundation Phase",
            Description = "Grades 1-3",
            StartGrade = 1,
            EndGrade = 3,
            MinimumTeachingHours = 800,
            MaximumTeachingHours = 1000
        };

        // Assert
        phase.Name.Should().Be("Foundation Phase");
        phase.Description.Should().Be("Grades 1-3");
        phase.StartGrade.Should().Be(1);
        phase.EndGrade.Should().Be(3);
        phase.MinimumTeachingHours.Should().Be(800);
        phase.MaximumTeachingHours.Should().Be(1000);
    }

    [Fact]
    public void AddGradeCurriculum_ShouldSupportMultipleCurricula()
    {
        // Arrange
        var phase = new EducationalPhaseEntity();
        var grade1 = new GradeCurriculumEntity { Grade = 1 };
        var grade2 = new GradeCurriculumEntity { Grade = 2 };
        var grade3 = new GradeCurriculumEntity { Grade = 3 };

        // Act
        phase.AddGradeCurriculum(grade1);
        phase.AddGradeCurriculum(grade2);
        phase.AddGradeCurriculum(grade3);

        // Assert
        phase.GradeCurricula.Should().HaveCount(3);
        phase.GradeCurricula.Should().Contain(new[] { grade1, grade2, grade3 });
    }
}
