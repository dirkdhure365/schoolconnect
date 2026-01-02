using SchoolConnect.Curriculum.Domain.Entities;
using SchoolConnect.Curriculum.Domain.Enums;
using FluentAssertions;

namespace SchoolConnect.Curriculum.Domain.Tests.Entities;

public class ResourceEntitiesTests
{
    [Fact]
    public void ResourceEntity_Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var resource = new ResourceEntity();

        // Assert
        resource.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void ResourceEntity_Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var resource = new ResourceEntity
        {
            Name = "Mathematics Textbook",
            Category = ResourceCategory.Textbook,
            Description = "Official Grade 10 Mathematics textbook",
            IsRequired = true,
            Provider = "Publisher XYZ",
            Url = "https://example.com/textbook",
            Isbn = "978-1-234567-89-0"
        };

        // Assert
        resource.Name.Should().Be("Mathematics Textbook");
        resource.Category.Should().Be(ResourceCategory.Textbook);
        resource.Description.Should().Be("Official Grade 10 Mathematics textbook");
        resource.IsRequired.Should().BeTrue();
        resource.Provider.Should().Be("Publisher XYZ");
        resource.Url.Should().Be("https://example.com/textbook");
        resource.Isbn.Should().Be("978-1-234567-89-0");
    }

    [Fact]
    public void ProjectEntity_Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var project = new ProjectEntity();

        // Assert
        project.Id.Should().NotBe(Guid.Empty);
        project.Phases.Should().BeEmpty();
        project.Criteria.Should().BeEmpty();
        project.TopicIds.Should().BeEmpty();
    }

    [Fact]
    public void ProjectEntity_AddPhase_ShouldAddToCollection()
    {
        // Arrange
        var project = new ProjectEntity();
        var phase = new ProjectPhaseEntity
        {
            Name = "Planning",
            Sequence = 1,
            DurationWeeks = 2
        };

        // Act
        project.AddPhase(phase);

        // Assert
        project.Phases.Should().ContainSingle();
        project.Phases.Should().Contain(phase);
    }

    [Fact]
    public void ProjectEntity_AddCriterion_ShouldAddToCollection()
    {
        // Arrange
        var project = new ProjectEntity();
        var criterion = new ProjectCriterionEntity
        {
            Name = "Research Quality",
            Marks = 20,
            CognitiveLevel = CognitiveLevel.Analysis
        };

        // Act
        project.AddCriterion(criterion);

        // Assert
        project.Criteria.Should().ContainSingle();
        project.Criteria.Should().Contain(criterion);
    }

    [Fact]
    public void ProjectEntity_AddTopic_ShouldAddToCollection()
    {
        // Arrange
        var project = new ProjectEntity();
        var topicId = Guid.NewGuid();

        // Act
        project.AddTopic(topicId);

        // Assert
        project.TopicIds.Should().ContainSingle();
        project.TopicIds.Should().Contain(topicId);
    }

    [Fact]
    public void PracticalAssessmentEntity_Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var assessment = new PracticalAssessmentEntity();

        // Assert
        assessment.Id.Should().NotBe(Guid.Empty);
        assessment.Phases.Should().BeEmpty();
        assessment.ProgrammingComponents.Should().BeEmpty();
        assessment.Criteria.Should().BeEmpty();
        assessment.TopicIds.Should().BeEmpty();
    }

    [Fact]
    public void PracticalAssessmentEntity_AddPhase_ShouldAddToCollection()
    {
        // Arrange
        var assessment = new PracticalAssessmentEntity();
        var phase = new PracticalPhaseEntity
        {
            Name = "Development",
            Term = SchoolTerm.Term2,
            Marks = 50,
            Sequence = 1
        };

        // Act
        assessment.AddPhase(phase);

        // Assert
        assessment.Phases.Should().ContainSingle();
        assessment.Phases.Should().Contain(phase);
    }

    [Fact]
    public void PracticalAssessmentEntity_AddProgrammingComponent_ShouldAddToCollection()
    {
        // Arrange
        var assessment = new PracticalAssessmentEntity();
        var component = new ProgrammingComponentEntity
        {
            Name = "User Interface",
            Type = ComponentType.Component,
            IsMandatory = true
        };

        // Act
        assessment.AddProgrammingComponent(component);

        // Assert
        assessment.ProgrammingComponents.Should().ContainSingle();
        assessment.ProgrammingComponents.Should().Contain(component);
    }

    [Fact]
    public void PracticalAssessmentEntity_AddCriterion_ShouldAddToCollection()
    {
        // Arrange
        var assessment = new PracticalAssessmentEntity();
        var criterion = new PracticalCriterionEntity
        {
            Name = "Code Quality",
            Marks = 30,
            CognitiveLevel = CognitiveLevel.Synthesis
        };

        // Act
        assessment.AddCriterion(criterion);

        // Assert
        assessment.Criteria.Should().ContainSingle();
        assessment.Criteria.Should().Contain(criterion);
    }

    [Fact]
    public void GlossaryEntity_Constructor_ShouldGenerateNewId()
    {
        // Arrange & Act
        var glossary = new GlossaryEntity();

        // Assert
        glossary.Id.Should().NotBe(Guid.Empty);
        glossary.Terms.Should().BeEmpty();
    }

    [Fact]
    public void GlossaryEntity_AddTerm_ShouldAddToCollection()
    {
        // Arrange
        var glossary = new GlossaryEntity();
        var term = new GlossaryTermEntity
        {
            Term = "Algorithm",
            Definition = "A step-by-step procedure for solving a problem",
            Context = "Computer Science"
        };

        // Act
        glossary.AddTerm(term);

        // Assert
        glossary.Terms.Should().ContainSingle();
        glossary.Terms.Should().Contain(term);
    }

    [Fact]
    public void GlossaryTermEntity_AddRelatedTerm_ShouldAddToCollection()
    {
        // Arrange
        var term = new GlossaryTermEntity
        {
            Term = "Algorithm",
            Definition = "A step-by-step procedure"
        };

        // Act
        term.AddRelatedTerm("Pseudocode");
        term.AddRelatedTerm("Flowchart");

        // Assert
        term.RelatedTerms.Should().NotBeNull();
        term.RelatedTerms.Should().HaveCount(2);
        term.RelatedTerms.Should().Contain("Pseudocode");
        term.RelatedTerms.Should().Contain("Flowchart");
    }

    [Fact]
    public void GlossaryTermEntity_RelatedTerms_ShouldReturnNull_WhenEmpty()
    {
        // Arrange & Act
        var term = new GlossaryTermEntity();

        // Assert
        term.RelatedTerms.Should().BeNull();
    }

    [Fact]
    public void ProjectPhaseEntity_Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var phase = new ProjectPhaseEntity
        {
            Name = "Research Phase",
            Description = "Initial research and planning",
            Sequence = 1,
            DurationWeeks = 3
        };

        // Assert
        phase.Id.Should().NotBe(Guid.Empty);
        phase.Name.Should().Be("Research Phase");
        phase.Description.Should().Be("Initial research and planning");
        phase.Sequence.Should().Be(1);
        phase.DurationWeeks.Should().Be(3);
    }

    [Fact]
    public void ProgrammingComponentEntity_Properties_ShouldBeSettable()
    {
        // Arrange & Act
        var component = new ProgrammingComponentEntity
        {
            Name = "Database Component",
            Type = ComponentType.DataStructure,
            Description = "Database design and implementation",
            IsMandatory = true,
            Examples = "SQL Server, MySQL, SQLite"
        };

        // Assert
        component.Id.Should().NotBe(Guid.Empty);
        component.Name.Should().Be("Database Component");
        component.Type.Should().Be(ComponentType.DataStructure);
        component.Description.Should().Be("Database design and implementation");
        component.IsMandatory.Should().BeTrue();
        component.Examples.Should().Be("SQL Server, MySQL, SQLite");
    }
}
