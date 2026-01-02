using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.DTOs;

public class StaffMemberDto
{
    public Guid Id { get; set; }
    public Guid InstituteId { get; set; }
    public Guid UserId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? JobTitle { get; set; }
    public string? Department { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public DateTime JoinDate { get; set; }
    public StaffStatus Status { get; set; }
    public List<string> Qualifications { get; set; } = [];
    public List<string> Specializations { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}

public class StaffSummaryDto
{
    public Guid Id { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? JobTitle { get; set; }
    public StaffStatus Status { get; set; }
}

public class TeamDto
{
    public Guid Id { get; set; }
    public Guid InstituteId { get; set; }
    public Guid? CentreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TeamType Type { get; set; }
    public Guid? LeaderId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class TeamMemberDto
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public Guid StaffMemberId { get; set; }
    public string? Role { get; set; }
    public DateTime JoinedAt { get; set; }
    public DateTime? LeftAt { get; set; }
}
