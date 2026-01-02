using MongoDB.Driver;
using SchoolConnect.LessonDelivery.Domain.Entities;

namespace SchoolConnect.LessonDelivery.Infrastructure.Persistence;

public class LessonDeliveryDbContext
{
    private readonly IMongoDatabase _database;

    public LessonDeliveryDbContext(IMongoClient mongoClient, string databaseName = "SchoolConnectLessonDelivery")
    {
        _database = mongoClient.GetDatabase(databaseName);
    }

    public IMongoCollection<LessonPlan> LessonPlans => _database.GetCollection<LessonPlan>("lesson_plans");
    public IMongoCollection<LessonTemplate> LessonTemplates => _database.GetCollection<LessonTemplate>("lesson_templates");
    public IMongoCollection<ScheduledLesson> ScheduledLessons => _database.GetCollection<ScheduledLesson>("scheduled_lessons");
    public IMongoCollection<LessonSession> LessonSessions => _database.GetCollection<LessonSession>("lesson_sessions");
    public IMongoCollection<LessonArtifact> LessonArtifacts => _database.GetCollection<LessonArtifact>("lesson_artifacts");
    public IMongoCollection<Attendance> Attendances => _database.GetCollection<Attendance>("attendances");
    public IMongoCollection<Homework> Homeworks => _database.GetCollection<Homework>("homeworks");
    public IMongoCollection<HomeworkSubmission> HomeworkSubmissions => _database.GetCollection<HomeworkSubmission>("homework_submissions");
    public IMongoCollection<CurriculumCoverage> CurriculumCoverages => _database.GetCollection<CurriculumCoverage>("curriculum_coverages");
}
