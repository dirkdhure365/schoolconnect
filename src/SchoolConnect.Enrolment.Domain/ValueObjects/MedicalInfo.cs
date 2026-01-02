using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.ValueObjects;

public class MedicalInfo : ValueObject
{
    public string? BloodType { get; private set; }
    public List<string> Allergies { get; private set; } = [];
    public List<string> ChronicConditions { get; private set; } = [];
    public List<string> Medications { get; private set; } = [];
    public string? DoctorName { get; private set; }
    public string? DoctorPhone { get; private set; }
    public string? MedicalAidName { get; private set; }
    public string? MedicalAidNumber { get; private set; }
    public string? SpecialNeeds { get; private set; }
    public string? DietaryRestrictions { get; private set; }

    private MedicalInfo() { }

    public MedicalInfo(
        string? bloodType = null,
        List<string>? allergies = null,
        List<string>? chronicConditions = null,
        List<string>? medications = null,
        string? doctorName = null,
        string? doctorPhone = null,
        string? medicalAidName = null,
        string? medicalAidNumber = null,
        string? specialNeeds = null,
        string? dietaryRestrictions = null)
    {
        BloodType = bloodType;
        Allergies = allergies ?? [];
        ChronicConditions = chronicConditions ?? [];
        Medications = medications ?? [];
        DoctorName = doctorName;
        DoctorPhone = doctorPhone;
        MedicalAidName = medicalAidName;
        MedicalAidNumber = medicalAidNumber;
        SpecialNeeds = specialNeeds;
        DietaryRestrictions = dietaryRestrictions;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        if (BloodType != null) yield return BloodType;
        foreach (var allergy in Allergies) yield return allergy;
        foreach (var condition in ChronicConditions) yield return condition;
        foreach (var medication in Medications) yield return medication;
        if (DoctorName != null) yield return DoctorName;
        if (DoctorPhone != null) yield return DoctorPhone;
        if (MedicalAidName != null) yield return MedicalAidName;
        if (MedicalAidNumber != null) yield return MedicalAidNumber;
        if (SpecialNeeds != null) yield return SpecialNeeds;
        if (DietaryRestrictions != null) yield return DietaryRestrictions;
    }
}
