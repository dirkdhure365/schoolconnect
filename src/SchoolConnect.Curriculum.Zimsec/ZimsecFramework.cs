using SchoolConnect.Curriculum.Domain.Entities;

namespace SchoolConnect.Curriculum.Zimsec;

/// <summary>
/// ZIMSEC (Zimbabwe School Examinations Council) framework.
/// </summary>
public class ZimsecFramework : CurriculumFrameworkBase
{
    public ZimsecFramework()
    {
        Code = "ZIMSEC";
        Name = "Zimbabwe School Examinations Council Curriculum";
        Country = "Zimbabwe";
        ExaminationBoard = "Zimbabwe School Examinations Council";
        Version = "2015";
        EffectiveDate = new DateTime(2015, 1, 1);
        Description = "The Zimbabwe School Examinations Council (ZIMSEC) curriculum framework.";

        InitializePrinciples();
        InitializeGeneralAims();
    }

    private void InitializePrinciples()
    {
        AddPrinciple("Relevance: ensuring that education is relevant to the needs of learners and society");
        AddPrinciple("Equity: providing equal access to quality education for all learners");
        AddPrinciple("Inclusivity: accommodating learners with diverse needs and abilities");
        AddPrinciple("Progression: ensuring systematic progression from simple to complex concepts");
        AddPrinciple("Integration: promoting integration of knowledge across subjects");
        AddPrinciple("Practical orientation: emphasizing practical application of knowledge");
    }

    private void InitializeGeneralAims()
    {
        AddGeneralAim("Develop critical thinking and problem-solving skills");
        AddGeneralAim("Foster creativity and innovation");
        AddGeneralAim("Promote effective communication skills");
        AddGeneralAim("Develop research and information literacy");
        AddGeneralAim("Cultivate values of patriotism and national identity");
        AddGeneralAim("Prepare learners for further education and employment");
    }
}
