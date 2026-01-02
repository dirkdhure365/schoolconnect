using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Interfaces;
using SchoolConnect.Institution.Domain.ValueObjects;

namespace SchoolConnect.Institution.Infrastructure.Seed;

public class InstitutionSeedService
{
    private readonly IInstituteRepository _instituteRepository;
    private readonly ICentreRepository _centreRepository;
    
    public InstitutionSeedService(
        IInstituteRepository instituteRepository,
        ICentreRepository centreRepository)
    {
        _instituteRepository = instituteRepository;
        _centreRepository = centreRepository;
    }
    
    public async Task SeedAsync()
    {
        // Check if data already exists
        var institutes = await _instituteRepository.GetAllAsync();
        if (institutes.Any()) return;
        
        // Create sample institute
        var contactInfo = new ContactInfo("info@example.edu", "+27123456789", "https://example.edu");
        var address = new Address("123 Main Street", "Cape Town", "Western Cape", "8001", "South Africa");
        
        var institute = Institute.Create(
            "Example High School",
            "EHS001",
            InstituteType.School,
            contactInfo,
            address,
            "South Africa",
            "Africa/Johannesburg",
            1,
            "A premier educational institution",
            null
        );
        
        institute.Activate();
        await _instituteRepository.AddAsync(institute);
        
        // Create sample centre
        var centreContact = new ContactInfo("centre1@example.edu", "+27123456780", null);
        var centreAddress = new Address("123 Main Street", "Cape Town", "Western Cape", "8001", "South Africa");
        var workingHours = new WorkingHours(
            new TimeOnly(8, 0),
            new TimeOnly(15, 0),
            new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
        );
        
        var centre = Centre.Create(
            institute.Id,
            "Main Campus",
            "MC001",
            centreAddress,
            centreContact,
            500,
            workingHours,
            new GeoLocation(-33.9249, 18.4241)
        );
        
        await _centreRepository.AddAsync(centre);
    }
}
