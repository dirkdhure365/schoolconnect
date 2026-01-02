using MongoDB.Driver;
using ApplicationEntity = SchoolConnect.Enrolment.Domain.Entities.Application;
using SchoolConnect.Enrolment.Domain.Entities;

namespace SchoolConnect.Enrolment.Infrastructure.Persistence;

public class EnrolmentDbContext
{
    private readonly IMongoDatabase _database;

    public EnrolmentDbContext(IMongoClient mongoClient, string databaseName = "SchoolConnectEnrolment")
    {
        _database = mongoClient.GetDatabase(databaseName);
    }

    public IMongoCollection<AdmissionPeriod> AdmissionPeriods => _database.GetCollection<AdmissionPeriod>("admission_periods");
    public IMongoCollection<ApplicationEntity> Applications => _database.GetCollection<ApplicationEntity>("applications");
    public IMongoCollection<Student> Students => _database.GetCollection<Student>("students");
    public IMongoCollection<ParentStudent> ParentStudents => _database.GetCollection<ParentStudent>("parent_students");
    public IMongoCollection<Domain.Entities.Stream> Streams => _database.GetCollection<Domain.Entities.Stream>("streams");
    public IMongoCollection<Cohort> Cohorts => _database.GetCollection<Cohort>("cohorts");
    public IMongoCollection<Class> Classes => _database.GetCollection<Class>("classes");
    public IMongoCollection<StudentEnrolment> StudentEnrolments => _database.GetCollection<StudentEnrolment>("student_enrolments");
    public IMongoCollection<StudentClassEnrolment> StudentClassEnrolments => _database.GetCollection<StudentClassEnrolment>("student_class_enrolments");
    public IMongoCollection<ExtraCurricularEnrolment> ExtraCurricularEnrolments => _database.GetCollection<ExtraCurricularEnrolment>("extra_curricular_enrolments");
    public IMongoCollection<StudentBillingAccount> StudentBillingAccounts => _database.GetCollection<StudentBillingAccount>("student_billing_accounts");
}
