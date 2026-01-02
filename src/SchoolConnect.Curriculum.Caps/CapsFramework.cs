using SchoolConnect.Curriculum.Domain.Entities;

namespace SchoolConnect.Curriculum.Caps;

/// <summary>
/// CAPS (Curriculum and Assessment Policy Statement) framework for South Africa.
/// </summary>
public class CapsFramework : CurriculumFrameworkBase
{
    public CapsFramework()
    {
        Code = "CAPS";
        Name = "Curriculum and Assessment Policy Statement";
        Country = "South Africa";
        ExaminationBoard = "Department of Basic Education";
        Version = "2011";
        EffectiveDate = new DateTime(2012, 1, 1);
        Description = "The Curriculum and Assessment Policy Statement (CAPS) for South Africa, implemented from 2012.";

        InitializePrinciples();
        InitializeGeneralAims();
    }

    private void InitializePrinciples()
    {
        AddPrinciple("Social transformation: ensuring that the educational imbalances of the past are redressed");
        AddPrinciple("Active and critical learning: encouraging an active and critical approach to learning");
        AddPrinciple("High knowledge and high skills: the minimum standards of knowledge and skills to be achieved");
        AddPrinciple("Progression: content and context showing progression from simple to complex");
        AddPrinciple("Social and environmental justice: infusing the principles and practices of social and environmental justice");
        AddPrinciple("Valuing indigenous knowledge systems: acknowledging the rich history and heritage of this country");
        AddPrinciple("Credibility, quality and efficiency: providing an education that is globally comparable in quality");
    }

    private void InitializeGeneralAims()
    {
        AddGeneralAim("Identify and solve problems using critical and creative thinking");
        AddGeneralAim("Work effectively with others as members of a team, group, organisation and community");
        AddGeneralAim("Organise and manage oneself and one's activities responsibly and effectively");
        AddGeneralAim("Collect, analyse, organise and critically evaluate information");
        AddGeneralAim("Communicate effectively using visual, symbolic and/or language skills");
        AddGeneralAim("Use science and technology effectively and critically");
        AddGeneralAim("Demonstrate an understanding of the world as a set of related systems");
    }
}
