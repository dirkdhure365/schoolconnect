using SchoolConnect.Curriculum.Zimsec;
using FluentAssertions;

namespace SchoolConnect.Curriculum.Zimsec.Tests;

public class ZimsecFrameworkTests
{
    [Fact]
    public void Country_ShouldBeZimbabwe()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Country.Should().Be("Zimbabwe");
    }

    [Fact]
    public void Code_ShouldBeZIMSEC()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Code.Should().Be("ZIMSEC");
    }

    [Fact]
    public void ExaminationBoard_ShouldBeZimsec()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.ExaminationBoard.Should().Be("Zimbabwe School Examinations Council");
    }

    [Fact]
    public void Name_ShouldContainZimsec()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Name.Should().Contain("Zimbabwe School Examinations Council");
    }

    [Fact]
    public void Version_ShouldBe2015()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Version.Should().Be("2015");
    }

    [Fact]
    public void EffectiveDate_ShouldBe2015()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.EffectiveDate.Should().Be(new DateTime(2015, 1, 1));
    }

    [Fact]
    public void Principles_ShouldContainRelevance()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Principles.Should().Contain(p => p.Contains("Relevance"));
    }

    [Fact]
    public void Principles_ShouldContainEquity()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Principles.Should().Contain(p => p.Contains("Equity"));
    }

    [Fact]
    public void Principles_ShouldContainInclusivity()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Principles.Should().Contain(p => p.Contains("Inclusivity"));
    }

    [Fact]
    public void Principles_ShouldContainProgression()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Principles.Should().Contain(p => p.Contains("Progression"));
    }

    [Fact]
    public void Principles_ShouldHaveSixPrinciples()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Principles.Should().HaveCount(6);
    }

    [Fact]
    public void GeneralAims_ShouldContainCriticalThinking()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.GeneralAims.Should().Contain(a => a.Contains("critical thinking"));
    }

    [Fact]
    public void GeneralAims_ShouldContainCreativity()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.GeneralAims.Should().Contain(a => a.Contains("creativity"));
    }

    [Fact]
    public void GeneralAims_ShouldHaveSixAims()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.GeneralAims.Should().HaveCount(6);
    }

    [Fact]
    public void Id_ShouldBeGenerated()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Description_ShouldNotBeEmpty()
    {
        // Arrange & Act
        var framework = new ZimsecFramework();

        // Assert
        framework.Description.Should().NotBeNullOrEmpty();
        framework.Description.Should().Contain("ZIMSEC");
    }
}
