using SchoolConnect.Curriculum.Caps;
using FluentAssertions;

namespace SchoolConnect.Curriculum.Caps.Tests;

public class CapsFrameworkTests
{
    [Fact]
    public void Country_ShouldBeSouthAfrica()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Country.Should().Be("South Africa");
    }

    [Fact]
    public void ExaminationBoard_ShouldBeDBE()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.ExaminationBoard.Should().Be("Department of Basic Education");
    }

    [Fact]
    public void Code_ShouldBeCAPS()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Code.Should().Be("CAPS");
    }

    [Fact]
    public void Name_ShouldBeCurriculumAndAssessmentPolicyStatement()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Name.Should().Be("Curriculum and Assessment Policy Statement");
    }

    [Fact]
    public void Version_ShouldBe2011()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Version.Should().Be("2011");
    }

    [Fact]
    public void EffectiveDate_ShouldBe2012()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.EffectiveDate.Should().Be(new DateTime(2012, 1, 1));
    }

    [Fact]
    public void Principles_ShouldContainSocialTransformation()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Principles.Should().Contain(p => p.Contains("Social transformation"));
    }

    [Fact]
    public void Principles_ShouldContainActiveAndCriticalLearning()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Principles.Should().Contain(p => p.Contains("Active and critical learning"));
    }

    [Fact]
    public void Principles_ShouldContainHighKnowledgeAndSkills()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Principles.Should().Contain(p => p.Contains("High knowledge and high skills"));
    }

    [Fact]
    public void Principles_ShouldHaveSevenPrinciples()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Principles.Should().HaveCount(7);
    }

    [Fact]
    public void GeneralAims_ShouldHaveLearnerOutcomes()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.GeneralAims.Should().NotBeEmpty();
        framework.GeneralAims.Should().HaveCount(7);
    }

    [Fact]
    public void GeneralAims_ShouldIncludeProblemSolving()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.GeneralAims.Should().Contain(a => a.Contains("Identify and solve problems"));
    }

    [Fact]
    public void GeneralAims_ShouldIncludeTeamwork()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.GeneralAims.Should().Contain(a => a.Contains("Work effectively with others"));
    }

    [Fact]
    public void Id_ShouldBeGenerated()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Description_ShouldNotBeEmpty()
    {
        // Arrange & Act
        var framework = new CapsFramework();

        // Assert
        framework.Description.Should().NotBeNullOrEmpty();
        framework.Description.Should().Contain("CAPS");
    }
}
