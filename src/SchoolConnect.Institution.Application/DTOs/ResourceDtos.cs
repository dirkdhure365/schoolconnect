using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.DTOs;

public class ResourceDto
{
    public Guid Id { get; set; }
    public Guid CentreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ResourceType Type { get; set; }
    public string? SerialNumber { get; set; }
    public string? Description { get; set; }
    public ResourceCondition Condition { get; set; }
    public ResourceStatus Status { get; set; }
    public DateTime? AcquisitionDate { get; set; }
    public decimal? Value { get; set; }
    public string? Currency { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ResourceAllocationDto
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    public Guid AllocatedToId { get; set; }
    public string AllocatedToType { get; set; } = string.Empty;
    public string AllocatedToName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public AllocationStatus Status { get; set; }
}

public class ResourceInventoryReportDto
{
    public Guid CentreId { get; set; }
    public int TotalResources { get; set; }
    public int AvailableResources { get; set; }
    public int AllocatedResources { get; set; }
    public int UnderRepairResources { get; set; }
    public List<ResourceDto> Resources { get; set; } = [];
}
