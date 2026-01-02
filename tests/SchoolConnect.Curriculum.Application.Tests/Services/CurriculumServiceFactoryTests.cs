using SchoolConnect.Curriculum.Application.Services;
using SchoolConnect.Curriculum.Domain.Abstractions;
using SchoolConnect.Curriculum.Domain.Entities;
using FluentAssertions;
using Moq;

namespace SchoolConnect.Curriculum.Application.Tests.Services;

public class CurriculumServiceFactoryTests
{
    [Fact]
    public void RegisterService_ShouldRegisterServiceForBoard()
    {
        // Arrange
        var factory = new CurriculumServiceFactory();
        var mockService = new Mock<ICurriculumService>();
        var boardInfo = new BoardInfo
        {
            Code = "TEST",
            Name = "Test Board",
            Country = "Test Country",
            ExaminationBoard = "Test Board"
        };

        // Act
        factory.RegisterService("TEST", () => mockService.Object, boardInfo);
        var registeredBoards = factory.GetRegisteredBoards();

        // Assert
        registeredBoards.Should().Contain("TEST");
    }

    [Fact]
    public void CreateService_ShouldReturnCorrectService_ForValidCode()
    {
        // Arrange
        var factory = new CurriculumServiceFactory();
        var mockService = new Mock<ICurriculumService>();
        var boardInfo = new BoardInfo { Code = "TEST", Name = "Test" };
        factory.RegisterService("TEST", () => mockService.Object, boardInfo);

        // Act
        var service = factory.CreateService("TEST");

        // Assert
        service.Should().NotBeNull();
        service.Should().Be(mockService.Object);
    }

    [Fact]
    public void CreateService_ShouldThrowException_ForInvalidCode()
    {
        // Arrange
        var factory = new CurriculumServiceFactory();

        // Act & Assert
        var act = () => factory.CreateService("INVALID");
        act.Should().Throw<ArgumentException>()
            .WithMessage("*No curriculum service registered for board code: INVALID*");
    }

    [Fact]
    public void CreateService_ShouldBeCaseInsensitive()
    {
        // Arrange
        var factory = new CurriculumServiceFactory();
        var mockService = new Mock<ICurriculumService>();
        var boardInfo = new BoardInfo { Code = "TEST", Name = "Test" };
        factory.RegisterService("TEST", () => mockService.Object, boardInfo);

        // Act
        var service1 = factory.CreateService("test");
        var service2 = factory.CreateService("Test");
        var service3 = factory.CreateService("TEST");

        // Assert
        service1.Should().NotBeNull();
        service2.Should().NotBeNull();
        service3.Should().NotBeNull();
    }

    [Fact]
    public async Task GetBoardsAsync_ShouldReturnAllRegisteredBoards()
    {
        // Arrange
        var factory = new CurriculumServiceFactory();
        var mockService1 = new Mock<ICurriculumService>();
        var mockService2 = new Mock<ICurriculumService>();
        
        var boardInfo1 = new BoardInfo { Code = "CAPS", Name = "CAPS", Country = "South Africa" };
        var boardInfo2 = new BoardInfo { Code = "ZIMSEC", Name = "ZIMSEC", Country = "Zimbabwe" };

        factory.RegisterService("CAPS", () => mockService1.Object, boardInfo1);
        factory.RegisterService("ZIMSEC", () => mockService2.Object, boardInfo2);

        // Act
        var boards = await factory.GetBoardsAsync();

        // Assert
        boards.Should().HaveCount(2);
        boards.Should().Contain(b => b.Code == "CAPS");
        boards.Should().Contain(b => b.Code == "ZIMSEC");
    }

    [Fact]
    public async Task GetBoardsByCountryAsync_ShouldFilterCorrectly()
    {
        // Arrange
        var factory = new CurriculumServiceFactory();
        var mockService1 = new Mock<ICurriculumService>();
        var mockService2 = new Mock<ICurriculumService>();
        var mockService3 = new Mock<ICurriculumService>();
        
        var boardInfo1 = new BoardInfo { Code = "CAPS", Name = "CAPS", Country = "South Africa" };
        var boardInfo2 = new BoardInfo { Code = "IEB", Name = "IEB", Country = "South Africa" };
        var boardInfo3 = new BoardInfo { Code = "ZIMSEC", Name = "ZIMSEC", Country = "Zimbabwe" };

        factory.RegisterService("CAPS", () => mockService1.Object, boardInfo1);
        factory.RegisterService("IEB", () => mockService2.Object, boardInfo2);
        factory.RegisterService("ZIMSEC", () => mockService3.Object, boardInfo3);

        // Act
        var southAfricanBoards = await factory.GetBoardsByCountryAsync("South Africa");
        var zimbabweanBoards = await factory.GetBoardsByCountryAsync("Zimbabwe");

        // Assert
        southAfricanBoards.Should().HaveCount(2);
        southAfricanBoards.Should().Contain(b => b.Code == "CAPS");
        southAfricanBoards.Should().Contain(b => b.Code == "IEB");
        
        zimbabweanBoards.Should().ContainSingle();
        zimbabweanBoards.Should().Contain(b => b.Code == "ZIMSEC");
    }

    [Fact]
    public void GetRegisteredBoards_ShouldReturnEmpty_WhenNoBoardsRegistered()
    {
        // Arrange
        var factory = new CurriculumServiceFactory();

        // Act
        var boards = factory.GetRegisteredBoards();

        // Assert
        boards.Should().BeEmpty();
    }

    [Fact]
    public async Task GetBoardsByCountryAsync_ShouldReturnEmpty_WhenNoMatchingCountry()
    {
        // Arrange
        var factory = new CurriculumServiceFactory();
        var mockService = new Mock<ICurriculumService>();
        var boardInfo = new BoardInfo { Code = "CAPS", Name = "CAPS", Country = "South Africa" };
        factory.RegisterService("CAPS", () => mockService.Object, boardInfo);

        // Act
        var boards = await factory.GetBoardsByCountryAsync("Kenya");

        // Assert
        boards.Should().BeEmpty();
    }

    [Fact]
    public async Task GetBoardsByCountryAsync_ShouldBeCaseInsensitive()
    {
        // Arrange
        var factory = new CurriculumServiceFactory();
        var mockService = new Mock<ICurriculumService>();
        var boardInfo = new BoardInfo { Code = "CAPS", Name = "CAPS", Country = "South Africa" };
        factory.RegisterService("CAPS", () => mockService.Object, boardInfo);

        // Act
        var boards1 = await factory.GetBoardsByCountryAsync("south africa");
        var boards2 = await factory.GetBoardsByCountryAsync("SOUTH AFRICA");

        // Assert
        boards1.Should().ContainSingle();
        boards2.Should().ContainSingle();
    }
}
