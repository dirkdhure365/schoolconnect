using SchoolConnect.Curriculum.Application.Services;
using SchoolConnect.Curriculum.Caps.Repositories;
using SchoolConnect.Curriculum.Domain.Abstractions;
using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Caps.Services;

/// <summary>
/// CAPS curriculum service implementation.
/// </summary>
public class CapsCurriculumService : IPracticalCurriculumService
{
    private readonly ICapsRepository _repository;

    public CapsCurriculumService(ICapsRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Task<ICurriculumFramework> GetFrameworkAsync()
    {
        return _repository.GetFrameworkAsync();
    }

    public Task<IEnumerable<IEducationalPhase>> GetPhasesAsync()
    {
        return _repository.GetPhasesAsync();
    }

    public Task<IEducationalPhase?> GetPhaseByIdAsync(Guid phaseId)
    {
        return _repository.GetPhaseByIdAsync(phaseId);
    }

    public Task<IEnumerable<ISubject>> GetSubjectsAsync()
    {
        return _repository.GetSubjectsAsync();
    }

    public async Task<IEnumerable<ISubject>> GetSubjectsByPhaseAsync(Guid phaseId)
    {
        var framework = await _repository.GetFrameworkAsync();
        return framework.GetSubjectsByPhase(phaseId);
    }

    public async Task<IEnumerable<ISubject>> GetSubjectsByGradeAsync(int grade)
    {
        var framework = await _repository.GetFrameworkAsync();
        return framework.GetSubjectsByGrade(grade);
    }

    public Task<ISubject?> GetSubjectByIdAsync(Guid subjectId)
    {
        return _repository.GetSubjectByIdAsync(subjectId);
    }

    public Task<IEnumerable<ITopic>> GetTopicsBySubjectAsync(Guid subjectId)
    {
        return _repository.GetTopicsBySubjectAsync(subjectId);
    }

    public async Task<IEnumerable<ITopic>> GetTopicsBySubjectAndGradeAsync(Guid subjectId, int grade)
    {
        var topics = await _repository.GetTopicsBySubjectAsync(subjectId);
        return topics.Where(t => t.ApplicableGrades.Contains(grade));
    }

    public Task<ITopic?> GetTopicByIdAsync(Guid topicId)
    {
        return _repository.GetTopicByIdAsync(topicId);
    }

    public Task<IGradeCurriculum?> GetGradeCurriculumAsync(Guid subjectId, int grade)
    {
        return _repository.GetGradeCurriculumAsync(subjectId, grade);
    }

    public async Task<IEnumerable<ITermPlan>> GetTermPlansAsync(Guid subjectId, int grade)
    {
        var curriculum = await _repository.GetGradeCurriculumAsync(subjectId, grade);
        return curriculum?.TermPlans ?? Enumerable.Empty<ITermPlan>();
    }

    public async Task<IAssessmentPolicy?> GetAssessmentPolicyAsync(Guid subjectId)
    {
        var subject = await _repository.GetSubjectByIdAsync(subjectId);
        return subject?.AssessmentPolicy;
    }

    public async Task<IGradeAssessmentRequirements?> GetGradeAssessmentRequirementsAsync(Guid subjectId, int grade)
    {
        var curriculum = await _repository.GetGradeCurriculumAsync(subjectId, grade);
        return curriculum?.AssessmentRequirements;
    }

    public async Task<IEnumerable<IResource>> GetResourcesAsync(Guid subjectId)
    {
        var subject = await _repository.GetSubjectByIdAsync(subjectId);
        return subject?.Resources ?? Enumerable.Empty<IResource>();
    }

    public async Task<IGlossary?> GetGlossaryAsync(Guid subjectId)
    {
        var subject = await _repository.GetSubjectByIdAsync(subjectId);
        return subject?.Glossary;
    }

    public async Task<IEnumerable<IContentItem>> SearchContentAsync(string query, int? grade = null)
    {
        var subjects = await _repository.GetSubjectsAsync();
        var results = new List<IContentItem>();

        foreach (var subject in subjects)
        {
            var topics = await _repository.GetTopicsBySubjectAsync(subject.Id);
            foreach (var topic in topics)
            {
                if (grade.HasValue && !topic.ApplicableGrades.Contains(grade.Value))
                    continue;

                var matchingContent = topic.ContentItems
                    .Where(c => c.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                               (c.Description?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false));
                results.AddRange(matchingContent);

                foreach (var subTopic in topic.SubTopics)
                {
                    var subTopicContent = subTopic.ContentItems
                        .Where(c => c.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                   (c.Description?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false));
                    results.AddRange(subTopicContent);
                }
            }
        }

        return results;
    }

    public async Task<IEnumerable<ILearningObjective>> SearchObjectivesAsync(string query, CognitiveLevel? level = null)
    {
        var subjects = await _repository.GetSubjectsAsync();
        var results = new List<ILearningObjective>();

        foreach (var subject in subjects)
        {
            var topics = await _repository.GetTopicsBySubjectAsync(subject.Id);
            foreach (var topic in topics)
            {
                var matchingObjectives = topic.LearningObjectives
                    .Where(o => o.Description.Contains(query, StringComparison.OrdinalIgnoreCase) &&
                               (!level.HasValue || o.CognitiveLevel == level.Value));
                results.AddRange(matchingObjectives);

                foreach (var subTopic in topic.SubTopics)
                {
                    var subTopicObjectives = subTopic.LearningObjectives
                        .Where(o => o.Description.Contains(query, StringComparison.OrdinalIgnoreCase) &&
                                   (!level.HasValue || o.CognitiveLevel == level.Value));
                    results.AddRange(subTopicObjectives);
                }
            }
        }

        return results;
    }

    public Task<IPracticalAssessment?> GetPracticalAssessmentAsync(Guid subjectId, int grade)
    {
        return _repository.GetPracticalAssessmentAsync(subjectId, grade);
    }

    public Task<IEnumerable<IProject>> GetProjectsAsync(Guid subjectId, int grade)
    {
        return _repository.GetProjectsAsync(subjectId, grade);
    }

    public async Task<IEnumerable<IProgrammingComponent>> GetProgrammingComponentsAsync(Guid subjectId, int grade)
    {
        var pat = await _repository.GetPracticalAssessmentAsync(subjectId, grade);
        return pat?.ProgrammingComponents ?? Enumerable.Empty<IProgrammingComponent>();
    }
}
