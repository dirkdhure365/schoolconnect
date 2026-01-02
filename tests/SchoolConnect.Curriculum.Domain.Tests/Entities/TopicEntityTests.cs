using SchoolConnect.Curriculum.Domain.Entities;
using FluentAssertions;

namespace SchoolConnect.Curriculum.Domain.Tests.Entities;

public class TopicEntityTests
{
    [Fact]
    public void Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var topic = new TopicEntity();

        // Assert
        topic.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void SubTopics_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var topic = new TopicEntity();

        // Assert
        topic.SubTopics.Should().NotBeNull();
        topic.SubTopics.Should().BeEmpty();
    }

    [Fact]
    public void ContentItems_ShouldSupportNestedItems()
    {
        // Arrange
        var topic = new TopicEntity { Name = "Algebra" };
        var content1 = new ContentItemEntity { Title = "Introduction", Sequence = 1 };
        var content2 = new ContentItemEntity { Title = "Advanced Topics", Sequence = 2 };

        // Act
        topic.AddContentItem(content1);
        topic.AddContentItem(content2);

        // Assert
        topic.ContentItems.Should().HaveCount(2);
        topic.ContentItems.Should().Contain(new[] { content1, content2 });
    }

    [Fact]
    public void LinkedTopicIds_ShouldAllowCrossReferencing()
    {
        // Arrange
        var topic = new TopicEntity { Name = "Topic 1" };
        var relatedTopicId1 = Guid.NewGuid();
        var relatedTopicId2 = Guid.NewGuid();

        // Act
        topic.AddLinkedTopic(relatedTopicId1);
        topic.AddLinkedTopic(relatedTopicId2);

        // Assert
        topic.LinkedTopicIds.Should().NotBeNull();
        topic.LinkedTopicIds.Should().HaveCount(2);
        topic.LinkedTopicIds.Should().Contain(new[] { relatedTopicId1, relatedTopicId2 });
    }

    [Fact]
    public void LinkedTopicIds_ShouldReturnNull_WhenEmpty()
    {
        // Arrange & Act
        var topic = new TopicEntity();

        // Assert
        topic.LinkedTopicIds.Should().BeNull();
    }

    [Fact]
    public void AddSubTopic_ShouldAddToCollection()
    {
        // Arrange
        var topic = new TopicEntity { Name = "Main Topic" };
        var subTopic = new SubTopicEntity { Name = "Sub Topic 1" };

        // Act
        topic.AddSubTopic(subTopic);

        // Assert
        topic.SubTopics.Should().ContainSingle();
        topic.SubTopics.Should().Contain(subTopic);
    }

    [Fact]
    public void AddLearningObjective_ShouldAddToCollection()
    {
        // Arrange
        var topic = new TopicEntity();
        var objective = new LearningObjectiveEntity { Description = "Understand concepts" };

        // Act
        topic.AddLearningObjective(objective);

        // Assert
        topic.LearningObjectives.Should().ContainSingle();
        topic.LearningObjectives.Should().Contain(objective);
    }

    [Fact]
    public void AddApplicableGrade_ShouldAddToCollection()
    {
        // Arrange
        var topic = new TopicEntity();

        // Act
        topic.AddApplicableGrade(10);
        topic.AddApplicableGrade(11);

        // Assert
        topic.ApplicableGrades.Should().HaveCount(2);
        topic.ApplicableGrades.Should().Contain(new[] { 10, 11 });
    }

    [Fact]
    public void Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var subjectId = Guid.NewGuid();
        var topic = new TopicEntity
        {
            Name = "Algebra",
            Code = "ALG",
            Description = "Basic Algebra",
            SubjectId = subjectId,
            RecommendedHours = 20,
            ContentWeighting = 0.25m
        };

        // Assert
        topic.Name.Should().Be("Algebra");
        topic.Code.Should().Be("ALG");
        topic.Description.Should().Be("Basic Algebra");
        topic.SubjectId.Should().Be(subjectId);
        topic.RecommendedHours.Should().Be(20);
        topic.ContentWeighting.Should().Be(0.25m);
    }

    [Fact]
    public void AddTopicProgression_ShouldAddToCollection()
    {
        // Arrange
        var topic = new TopicEntity();
        var progression = new TopicProgressionEntity 
        { 
            Grade = 10, 
            Description = "Introduction level" 
        };

        // Act
        topic.AddTopicProgression(progression);

        // Assert
        topic.TopicProgressions.Should().ContainSingle();
        topic.TopicProgressions.Should().Contain(progression);
    }
}
