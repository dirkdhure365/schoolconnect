using SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Domain.Entities;

namespace SchoolConnect.EducationSystem.Infrastructure.Seed;

public class DataSeeder
{
    private readonly IRepository<Country> _countryRepo;
    private readonly IRepository<Domain.Entities.EducationSystem> _educationSystemRepo;
    private readonly IRepository<AssessmentBoard> _assessmentBoardRepo;
    private readonly IRepository<EducationPhase> _educationPhaseRepo;
    private readonly IRepository<Domain.Entities.Program> _programRepo;
    private readonly IRepository<Subject> _subjectRepo;
    private readonly IRepository<Domain.Entities.Curriculum> _curriculumRepo;

    public DataSeeder(
        IRepository<Country> countryRepo,
        IRepository<Domain.Entities.EducationSystem> educationSystemRepo,
        IRepository<AssessmentBoard> assessmentBoardRepo,
        IRepository<EducationPhase> educationPhaseRepo,
        IRepository<Domain.Entities.Program> programRepo,
        IRepository<Subject> subjectRepo,
        IRepository<Domain.Entities.Curriculum> curriculumRepo)
    {
        _countryRepo = countryRepo;
        _educationSystemRepo = educationSystemRepo;
        _assessmentBoardRepo = assessmentBoardRepo;
        _educationPhaseRepo = educationPhaseRepo;
        _programRepo = programRepo;
        _subjectRepo = subjectRepo;
        _curriculumRepo = curriculumRepo;
    }

    public async Task SeedDataAsync()
    {
        await SeedZimbabweAsync();
        await SeedSouthAfricaAsync();
        await SeedBotswanaAsync();
        await SeedZambiaAsync();
    }

    private async Task SeedZimbabweAsync()
    {
        var zimbabwe = Country.Create("Zimbabwe", "ZW", "Southern Africa");
        await _countryRepo.AddAsync(zimbabwe);

        var eduSystem = Domain.Entities.EducationSystem.Create(zimbabwe.Id, "Zimbabwe Education System", "National education framework for Zimbabwe");
        await _educationSystemRepo.AddAsync(eduSystem);

        var zimsec = AssessmentBoard.Create(eduSystem.Id, "Zimbabwe School Examinations Council", "ZIMSEC", "National examination board for Zimbabwe");
        await _assessmentBoardRepo.AddAsync(zimsec);

        // Primary Phase
        var primaryPhase = EducationPhase.Create(eduSystem.Id, "Primary Education", "Grades 1-7", 6, 12);
        await _educationPhaseRepo.AddAsync(primaryPhase);

        // Secondary Phase
        var secondaryPhase = EducationPhase.Create(eduSystem.Id, "Secondary Education", "Forms 1-6", 13, 18);
        await _educationPhaseRepo.AddAsync(secondaryPhase);

        // O Level Program (Forms 1-4)
        var oLevel = Domain.Entities.Program.Create(zimsec.Id, secondaryPhase.Id, "O Level", "Ordinary Level certification", 4);
        await _programRepo.AddAsync(oLevel);

        // O Level Subjects
        var oLevelSubjects = new[]
        {
            Subject.Create(oLevel.Id, "English Language", "ENG", "Core subject focusing on language skills", true),
            Subject.Create(oLevel.Id, "Mathematics", "MATH", "Core mathematics subject", true),
            Subject.Create(oLevel.Id, "Shona", "SHO", "Indigenous language", false),
            Subject.Create(oLevel.Id, "Ndebele", "NDE", "Indigenous language", false),
            Subject.Create(oLevel.Id, "Physics", "PHY", "Physical sciences", false),
            Subject.Create(oLevel.Id, "Chemistry", "CHEM", "Chemical sciences", false),
            Subject.Create(oLevel.Id, "Biology", "BIO", "Life sciences", false),
            Subject.Create(oLevel.Id, "Geography", "GEO", "Physical and human geography", false),
            Subject.Create(oLevel.Id, "History", "HIST", "World and Zimbabwean history", false),
            Subject.Create(oLevel.Id, "Commerce", "COM", "Business and commerce", false),
            Subject.Create(oLevel.Id, "Accounting", "ACC", "Financial accounting", false),
            Subject.Create(oLevel.Id, "Agriculture", "AGR", "Agricultural science", false),
            Subject.Create(oLevel.Id, "Computer Science", "CS", "Computing fundamentals", false)
        };

        foreach (var subject in oLevelSubjects)
        {
            await _subjectRepo.AddAsync(subject);
        }

        // A Level Program (Forms 5-6)
        var aLevel = Domain.Entities.Program.Create(zimsec.Id, secondaryPhase.Id, "A Level", "Advanced Level certification", 2);
        await _programRepo.AddAsync(aLevel);

        // A Level Subjects
        var aLevelSubjects = new[]
        {
            Subject.Create(aLevel.Id, "Mathematics", "MATH", "Advanced mathematics", false),
            Subject.Create(aLevel.Id, "Physics", "PHY", "Advanced physics", false),
            Subject.Create(aLevel.Id, "Chemistry", "CHEM", "Advanced chemistry", false),
            Subject.Create(aLevel.Id, "Biology", "BIO", "Advanced biology", false),
            Subject.Create(aLevel.Id, "Economics", "ECON", "Economic theory and practice", false),
            Subject.Create(aLevel.Id, "Business Studies", "BUS", "Business management", false),
            Subject.Create(aLevel.Id, "Accounting", "ACC", "Advanced accounting", false),
            Subject.Create(aLevel.Id, "Geography", "GEO", "Advanced geography", false),
            Subject.Create(aLevel.Id, "History", "HIST", "Advanced history", false),
            Subject.Create(aLevel.Id, "English Literature", "LIT", "Literature analysis", false),
            Subject.Create(aLevel.Id, "Computer Science", "CS", "Advanced computing", false)
        };

        foreach (var subject in aLevelSubjects)
        {
            await _subjectRepo.AddAsync(subject);
        }
    }

    private async Task SeedSouthAfricaAsync()
    {
        var southAfrica = Country.Create("South Africa", "ZA", "Southern Africa");
        await _countryRepo.AddAsync(southAfrica);

        var eduSystem = Domain.Entities.EducationSystem.Create(southAfrica.Id, "South African Education System", "Curriculum and Assessment Policy Statement (CAPS)");
        await _educationSystemRepo.AddAsync(eduSystem);

        var dbe = AssessmentBoard.Create(eduSystem.Id, "Department of Basic Education", "DBE", "National education department for South Africa");
        await _assessmentBoardRepo.AddAsync(dbe);

        // Foundation Phase (Grade R-3)
        var foundationPhase = EducationPhase.Create(eduSystem.Id, "Foundation Phase", "Grade R to 3", 5, 8);
        await _educationPhaseRepo.AddAsync(foundationPhase);

        var foundationProgram = Domain.Entities.Program.Create(dbe.Id, foundationPhase.Id, "Foundation Phase Program", "Early childhood education", 4);
        await _programRepo.AddAsync(foundationProgram);

        var foundationSubjects = new[]
        {
            Subject.Create(foundationProgram.Id, "Home Language", "HL", "Mother tongue instruction", true),
            Subject.Create(foundationProgram.Id, "First Additional Language", "FAL", "Second language", true),
            Subject.Create(foundationProgram.Id, "Mathematics", "MATH", "Numeracy skills", true),
            Subject.Create(foundationProgram.Id, "Life Skills", "LIFE", "Personal and social skills", true)
        };

        foreach (var subject in foundationSubjects)
        {
            await _subjectRepo.AddAsync(subject);
        }

        // Intermediate Phase (Grade 4-6)
        var intermediatePhase = EducationPhase.Create(eduSystem.Id, "Intermediate Phase", "Grade 4 to 6", 9, 11);
        await _educationPhaseRepo.AddAsync(intermediatePhase);

        var intermediateProgram = Domain.Entities.Program.Create(dbe.Id, intermediatePhase.Id, "Intermediate Phase Program", "Middle school education", 3);
        await _programRepo.AddAsync(intermediateProgram);

        var intermediateSubjects = new[]
        {
            Subject.Create(intermediateProgram.Id, "Home Language", "HL", "Mother tongue", true),
            Subject.Create(intermediateProgram.Id, "First Additional Language", "FAL", "Second language", true),
            Subject.Create(intermediateProgram.Id, "Mathematics", "MATH", "Mathematics", true),
            Subject.Create(intermediateProgram.Id, "Natural Sciences and Technology", "NST", "Science and technology", true),
            Subject.Create(intermediateProgram.Id, "Social Sciences", "SS", "Geography and history", true),
            Subject.Create(intermediateProgram.Id, "Life Skills", "LIFE", "Life orientation and arts", true)
        };

        foreach (var subject in intermediateSubjects)
        {
            await _subjectRepo.AddAsync(subject);
        }

        // Senior Phase (Grade 7-9)
        var seniorPhase = EducationPhase.Create(eduSystem.Id, "Senior Phase", "Grade 7 to 9", 12, 14);
        await _educationPhaseRepo.AddAsync(seniorPhase);

        var seniorProgram = Domain.Entities.Program.Create(dbe.Id, seniorPhase.Id, "Senior Phase Program", "Junior secondary education", 3);
        await _programRepo.AddAsync(seniorProgram);

        var seniorSubjects = new[]
        {
            Subject.Create(seniorProgram.Id, "Home Language", "HL", "Mother tongue", true),
            Subject.Create(seniorProgram.Id, "First Additional Language", "FAL", "Second language", true),
            Subject.Create(seniorProgram.Id, "Mathematics", "MATH", "Mathematics", true),
            Subject.Create(seniorProgram.Id, "Natural Sciences", "NS", "Physical and life sciences", true),
            Subject.Create(seniorProgram.Id, "Social Sciences", "SS", "Geography and history", true),
            Subject.Create(seniorProgram.Id, "Technology", "TECH", "Technology education", true),
            Subject.Create(seniorProgram.Id, "Economic and Management Sciences", "EMS", "Business studies", true),
            Subject.Create(seniorProgram.Id, "Life Orientation", "LO", "Life skills", true),
            Subject.Create(seniorProgram.Id, "Creative Arts", "ARTS", "Arts and culture", false)
        };

        foreach (var subject in seniorSubjects)
        {
            await _subjectRepo.AddAsync(subject);
        }

        // FET Phase (Grade 10-12) - NSC
        var fetPhase = EducationPhase.Create(eduSystem.Id, "Further Education and Training Phase", "Grade 10 to 12", 15, 18);
        await _educationPhaseRepo.AddAsync(fetPhase);

        var nsc = Domain.Entities.Program.Create(dbe.Id, fetPhase.Id, "National Senior Certificate", "Matric qualification", 3);
        await _programRepo.AddAsync(nsc);

        var nscSubjects = new[]
        {
            Subject.Create(nsc.Id, "Home Language", "HL", "Mother tongue", true),
            Subject.Create(nsc.Id, "First Additional Language", "FAL", "Second language", true),
            Subject.Create(nsc.Id, "Mathematics", "MATH", "Pure mathematics", false),
            Subject.Create(nsc.Id, "Mathematical Literacy", "ML", "Applied mathematics", false),
            Subject.Create(nsc.Id, "Life Orientation", "LO", "Life skills", true),
            Subject.Create(nsc.Id, "Physical Sciences", "PS", "Physics and chemistry", false),
            Subject.Create(nsc.Id, "Life Sciences", "LS", "Biology", false),
            Subject.Create(nsc.Id, "Accounting", "ACC", "Financial accounting", false),
            Subject.Create(nsc.Id, "Business Studies", "BUS", "Business management", false),
            Subject.Create(nsc.Id, "Economics", "ECON", "Economics", false),
            Subject.Create(nsc.Id, "Geography", "GEO", "Physical and human geography", false),
            Subject.Create(nsc.Id, "History", "HIST", "South African and world history", false),
            Subject.Create(nsc.Id, "Information Technology", "IT", "IT fundamentals", false),
            Subject.Create(nsc.Id, "Computer Applications Technology", "CAT", "Computer applications", false)
        };

        foreach (var subject in nscSubjects)
        {
            await _subjectRepo.AddAsync(subject);
        }
    }

    private async Task SeedBotswanaAsync()
    {
        var botswana = Country.Create("Botswana", "BW", "Southern Africa");
        await _countryRepo.AddAsync(botswana);

        var eduSystem = Domain.Entities.EducationSystem.Create(botswana.Id, "Botswana Education System", "National education framework for Botswana");
        await _educationSystemRepo.AddAsync(eduSystem);

        var bec = AssessmentBoard.Create(eduSystem.Id, "Botswana Examinations Council", "BEC", "National examination board for Botswana");
        await _assessmentBoardRepo.AddAsync(bec);

        // Junior Secondary Phase
        var juniorPhase = EducationPhase.Create(eduSystem.Id, "Junior Secondary", "Forms 1-3", 13, 15);
        await _educationPhaseRepo.AddAsync(juniorPhase);

        var jc = Domain.Entities.Program.Create(bec.Id, juniorPhase.Id, "Junior Certificate", "Junior secondary certification", 3);
        await _programRepo.AddAsync(jc);

        // Senior Secondary Phase
        var seniorPhase = EducationPhase.Create(eduSystem.Id, "Senior Secondary", "Forms 4-5", 16, 18);
        await _educationPhaseRepo.AddAsync(seniorPhase);

        var bgcse = Domain.Entities.Program.Create(bec.Id, seniorPhase.Id, "Botswana General Certificate of Secondary Education", "Senior secondary certification", 2);
        await _programRepo.AddAsync(bgcse);

        var bgcseSubjects = new[]
        {
            Subject.Create(bgcse.Id, "English Language", "ENG", "Core language subject", true),
            Subject.Create(bgcse.Id, "Setswana", "SET", "National language", true),
            Subject.Create(bgcse.Id, "Mathematics", "MATH", "Core mathematics", true),
            Subject.Create(bgcse.Id, "Combined Science", "SCI", "Integrated sciences", false),
            Subject.Create(bgcse.Id, "Geography", "GEO", "Physical and human geography", false),
            Subject.Create(bgcse.Id, "History", "HIST", "Botswana and world history", false),
            Subject.Create(bgcse.Id, "Commerce", "COM", "Business and commerce", false),
            Subject.Create(bgcse.Id, "Accounting", "ACC", "Financial accounting", false),
            Subject.Create(bgcse.Id, "Computer Studies", "CS", "Computing basics", false)
        };

        foreach (var subject in bgcseSubjects)
        {
            await _subjectRepo.AddAsync(subject);
        }
    }

    private async Task SeedZambiaAsync()
    {
        var zambia = Country.Create("Zambia", "ZM", "Southern Africa");
        await _countryRepo.AddAsync(zambia);

        var eduSystem = Domain.Entities.EducationSystem.Create(zambia.Id, "Zambian Education System", "National education framework for Zambia");
        await _educationSystemRepo.AddAsync(eduSystem);

        var ecz = AssessmentBoard.Create(eduSystem.Id, "Examinations Council of Zambia", "ECZ", "National examination board for Zambia");
        await _assessmentBoardRepo.AddAsync(ecz);

        // Junior Secondary
        var juniorPhase = EducationPhase.Create(eduSystem.Id, "Junior Secondary", "Grades 8-9", 14, 15);
        await _educationPhaseRepo.AddAsync(juniorPhase);

        var jsle = Domain.Entities.Program.Create(ecz.Id, juniorPhase.Id, "Junior Secondary Leaving Examination", "Junior secondary certification", 2);
        await _programRepo.AddAsync(jsle);

        // Senior Secondary
        var seniorPhase = EducationPhase.Create(eduSystem.Id, "Senior Secondary", "Grades 10-12", 16, 18);
        await _educationPhaseRepo.AddAsync(seniorPhase);

        var sc = Domain.Entities.Program.Create(ecz.Id, seniorPhase.Id, "School Certificate", "Senior secondary certification", 3);
        await _programRepo.AddAsync(sc);

        var scSubjects = new[]
        {
            Subject.Create(sc.Id, "English Language", "ENG", "Core language subject", true),
            Subject.Create(sc.Id, "Mathematics", "MATH", "Core mathematics", true),
            Subject.Create(sc.Id, "Civic Education", "CIV", "Citizenship education", true),
            Subject.Create(sc.Id, "Combined Science", "SCI", "Integrated sciences", false),
            Subject.Create(sc.Id, "Geography", "GEO", "Physical and human geography", false),
            Subject.Create(sc.Id, "History", "HIST", "Zambian and world history", false),
            Subject.Create(sc.Id, "Commerce", "COM", "Business studies", false),
            Subject.Create(sc.Id, "Accounts", "ACC", "Financial accounting", false),
            Subject.Create(sc.Id, "Computer Studies", "CS", "Computing fundamentals", false),
            Subject.Create(sc.Id, "Religious Education", "RE", "Religious studies", false),
            Subject.Create(sc.Id, "Agricultural Science", "AGR", "Agricultural studies", false)
        };

        foreach (var subject in scSubjects)
        {
            await _subjectRepo.AddAsync(subject);
        }
    }
}
