using MediatR;
using SchoolConnect.Institution.Application.Commands.Resources;
using SchoolConnect.Institution.Application.Queries.Resources;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;

namespace SchoolConnect.Institution.Application.Handlers.Resources;

// Command Handlers
public class CreateResourceHandler : IRequestHandler<CreateResourceCommand, ResourceDto>
{
    private readonly IResourceRepository _repository;
    
    public CreateResourceHandler(IResourceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResourceDto> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = Resource.Create(
            request.CentreId,
            request.Name,
            request.Type,
            request.SerialNumber,
            request.Description,
            request.AcquisitionDate,
            request.Value,
            request.Currency,
            request.Location
        );
        
        await _repository.AddAsync(resource);
        
        return MapToDto(resource);
    }
    
    private static ResourceDto MapToDto(Resource resource) => new(
        resource.Id,
        resource.CentreId,
        resource.Name,
        resource.Type,
        resource.SerialNumber,
        resource.Description,
        resource.ImageUrl,
        resource.Condition,
        resource.Status,
        resource.AcquisitionDate,
        resource.Value,
        resource.Currency,
        resource.Location,
        resource.Attributes,
        resource.CreatedAt,
        resource.UpdatedAt
    );
}

public class UpdateResourceHandler : IRequestHandler<UpdateResourceCommand, ResourceDto>
{
    private readonly IResourceRepository _repository;
    
    public UpdateResourceHandler(IResourceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResourceDto> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.GetByIdAsync(request.Id);
        if (resource == null) throw new Exception($"Resource not found: {request.Id}");
        
        resource.Update(request.Name, request.Description, request.Value, request.Location);
        
        await _repository.UpdateAsync(resource);
        
        return MapToDto(resource);
    }
    
    private static ResourceDto MapToDto(Resource resource) => new(
        resource.Id,
        resource.CentreId,
        resource.Name,
        resource.Type,
        resource.SerialNumber,
        resource.Description,
        resource.ImageUrl,
        resource.Condition,
        resource.Status,
        resource.AcquisitionDate,
        resource.Value,
        resource.Currency,
        resource.Location,
        resource.Attributes,
        resource.CreatedAt,
        resource.UpdatedAt
    );
}

public class DeleteResourceHandler : IRequestHandler<DeleteResourceCommand, bool>
{
    private readonly IResourceRepository _repository;
    
    public DeleteResourceHandler(IResourceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<bool> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.GetByIdAsync(request.Id);
        if (resource == null) return false;
        
        await _repository.DeleteAsync(request.Id);
        
        return true;
    }
}

public class AllocateResourceHandler : IRequestHandler<AllocateResourceCommand, ResourceAllocationDto>
{
    private readonly IResourceAllocationRepository _allocationRepository;
    private readonly IResourceRepository _resourceRepository;
    
    public AllocateResourceHandler(IResourceAllocationRepository allocationRepository, IResourceRepository resourceRepository)
    {
        _allocationRepository = allocationRepository;
        _resourceRepository = resourceRepository;
    }
    
    public async Task<ResourceAllocationDto> Handle(AllocateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await _resourceRepository.GetByIdAsync(request.ResourceId);
        if (resource == null) throw new Exception($"Resource not found: {request.ResourceId}");
        
        if (resource.Status != Domain.Enums.ResourceStatus.Available)
        {
            throw new Exception("Resource is not available for allocation");
        }
        
        var allocation = ResourceAllocation.Create(
            request.ResourceId,
            request.AllocatedToId,
            request.AllocatedToType,
            request.AllocatedToName,
            request.AllocatedBy,
            request.StartDate,
            request.EndDate,
            request.Notes
        );
        
        resource.SetAllocated();
        
        await _allocationRepository.AddAsync(allocation);
        await _resourceRepository.UpdateAsync(resource);
        
        return MapToDto(allocation);
    }
    
    private static ResourceAllocationDto MapToDto(ResourceAllocation allocation) => new(
        allocation.Id,
        allocation.ResourceId,
        allocation.AllocatedToId,
        allocation.AllocatedToType,
        allocation.AllocatedToName,
        allocation.AllocatedBy,
        allocation.StartDate,
        allocation.EndDate,
        allocation.ReturnedDate,
        allocation.ConditionOnReturn,
        allocation.Notes,
        allocation.Status,
        allocation.CreatedAt
    );
}

public class ReturnResourceHandler : IRequestHandler<ReturnResourceCommand, ResourceAllocationDto>
{
    private readonly IResourceAllocationRepository _allocationRepository;
    private readonly IResourceRepository _resourceRepository;
    
    public ReturnResourceHandler(IResourceAllocationRepository allocationRepository, IResourceRepository resourceRepository)
    {
        _allocationRepository = allocationRepository;
        _resourceRepository = resourceRepository;
    }
    
    public async Task<ResourceAllocationDto> Handle(ReturnResourceCommand request, CancellationToken cancellationToken)
    {
        var allocation = await _allocationRepository.GetByIdAsync(request.AllocationId);
        if (allocation == null) throw new Exception($"Allocation not found: {request.AllocationId}");
        
        var resource = await _resourceRepository.GetByIdAsync(allocation.ResourceId);
        if (resource == null) throw new Exception($"Resource not found: {allocation.ResourceId}");
        
        allocation.Return(request.ConditionOnReturn, request.Notes);
        resource.UpdateCondition(request.ConditionOnReturn);
        resource.SetAvailable();
        
        await _allocationRepository.UpdateAsync(allocation);
        await _resourceRepository.UpdateAsync(resource);
        
        return MapToDto(allocation);
    }
    
    private static ResourceAllocationDto MapToDto(ResourceAllocation allocation) => new(
        allocation.Id,
        allocation.ResourceId,
        allocation.AllocatedToId,
        allocation.AllocatedToType,
        allocation.AllocatedToName,
        allocation.AllocatedBy,
        allocation.StartDate,
        allocation.EndDate,
        allocation.ReturnedDate,
        allocation.ConditionOnReturn,
        allocation.Notes,
        allocation.Status,
        allocation.CreatedAt
    );
}

public class ReportResourceDamageHandler : IRequestHandler<ReportResourceDamageCommand, ResourceAllocationDto>
{
    private readonly IResourceAllocationRepository _allocationRepository;
    private readonly IResourceRepository _resourceRepository;
    
    public ReportResourceDamageHandler(IResourceAllocationRepository allocationRepository, IResourceRepository resourceRepository)
    {
        _allocationRepository = allocationRepository;
        _resourceRepository = resourceRepository;
    }
    
    public async Task<ResourceAllocationDto> Handle(ReportResourceDamageCommand request, CancellationToken cancellationToken)
    {
        var allocation = await _allocationRepository.GetByIdAsync(request.AllocationId);
        if (allocation == null) throw new Exception($"Allocation not found: {request.AllocationId}");
        
        var resource = await _resourceRepository.GetByIdAsync(allocation.ResourceId);
        if (resource == null) throw new Exception($"Resource not found: {allocation.ResourceId}");
        
        allocation.ReportDamage(request.Condition, request.Notes);
        resource.UpdateCondition(request.Condition);
        
        await _allocationRepository.UpdateAsync(allocation);
        await _resourceRepository.UpdateAsync(resource);
        
        return MapToDto(allocation);
    }
    
    private static ResourceAllocationDto MapToDto(ResourceAllocation allocation) => new(
        allocation.Id,
        allocation.ResourceId,
        allocation.AllocatedToId,
        allocation.AllocatedToType,
        allocation.AllocatedToName,
        allocation.AllocatedBy,
        allocation.StartDate,
        allocation.EndDate,
        allocation.ReturnedDate,
        allocation.ConditionOnReturn,
        allocation.Notes,
        allocation.Status,
        allocation.CreatedAt
    );
}

// Query Handlers
public class GetResourceByIdHandler : IRequestHandler<GetResourceByIdQuery, ResourceDto?>
{
    private readonly IResourceRepository _repository;
    
    public GetResourceByIdHandler(IResourceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResourceDto?> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
    {
        var resource = await _repository.GetByIdAsync(request.Id);
        if (resource == null) return null;
        
        return MapToDto(resource);
    }
    
    private static ResourceDto MapToDto(Resource resource) => new(
        resource.Id,
        resource.CentreId,
        resource.Name,
        resource.Type,
        resource.SerialNumber,
        resource.Description,
        resource.ImageUrl,
        resource.Condition,
        resource.Status,
        resource.AcquisitionDate,
        resource.Value,
        resource.Currency,
        resource.Location,
        resource.Attributes,
        resource.CreatedAt,
        resource.UpdatedAt
    );
}

public class GetResourcesByCentreHandler : IRequestHandler<GetResourcesByCentreQuery, IEnumerable<ResourceDto>>
{
    private readonly IResourceRepository _repository;
    
    public GetResourcesByCentreHandler(IResourceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<ResourceDto>> Handle(GetResourcesByCentreQuery request, CancellationToken cancellationToken)
    {
        var resources = await _repository.GetByCentreIdAsync(request.CentreId);
        return resources.Select(MapToDto);
    }
    
    private static ResourceDto MapToDto(Resource resource) => new(
        resource.Id,
        resource.CentreId,
        resource.Name,
        resource.Type,
        resource.SerialNumber,
        resource.Description,
        resource.ImageUrl,
        resource.Condition,
        resource.Status,
        resource.AcquisitionDate,
        resource.Value,
        resource.Currency,
        resource.Location,
        resource.Attributes,
        resource.CreatedAt,
        resource.UpdatedAt
    );
}

public class GetResourceAllocationsHandler : IRequestHandler<GetResourceAllocationsQuery, IEnumerable<ResourceAllocationDto>>
{
    private readonly IResourceAllocationRepository _repository;
    
    public GetResourceAllocationsHandler(IResourceAllocationRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<ResourceAllocationDto>> Handle(GetResourceAllocationsQuery request, CancellationToken cancellationToken)
    {
        var allocations = await _repository.GetByResourceIdAsync(request.ResourceId);
        return allocations.Select(MapToDto);
    }
    
    private static ResourceAllocationDto MapToDto(ResourceAllocation allocation) => new(
        allocation.Id,
        allocation.ResourceId,
        allocation.AllocatedToId,
        allocation.AllocatedToType,
        allocation.AllocatedToName,
        allocation.AllocatedBy,
        allocation.StartDate,
        allocation.EndDate,
        allocation.ReturnedDate,
        allocation.ConditionOnReturn,
        allocation.Notes,
        allocation.Status,
        allocation.CreatedAt
    );
}

public class GetResourceInventoryReportHandler : IRequestHandler<GetResourceInventoryReportQuery, ResourceInventoryReportDto?>
{
    private readonly ICentreRepository _centreRepository;
    private readonly IResourceRepository _resourceRepository;
    
    public GetResourceInventoryReportHandler(ICentreRepository centreRepository, IResourceRepository resourceRepository)
    {
        _centreRepository = centreRepository;
        _resourceRepository = resourceRepository;
    }
    
    public async Task<ResourceInventoryReportDto?> Handle(GetResourceInventoryReportQuery request, CancellationToken cancellationToken)
    {
        var centre = await _centreRepository.GetByIdAsync(request.CentreId);
        if (centre == null) return null;
        
        var resources = await _resourceRepository.GetByCentreIdAsync(request.CentreId);
        var resourcesList = resources.ToList();
        
        var availableCount = resourcesList.Count(r => r.Status == Domain.Enums.ResourceStatus.Available);
        var allocatedCount = resourcesList.Count(r => r.Status == Domain.Enums.ResourceStatus.Allocated);
        var underRepairCount = resourcesList.Count(r => r.Status == Domain.Enums.ResourceStatus.UnderRepair);
        var lostCount = resourcesList.Count(r => r.Status == Domain.Enums.ResourceStatus.Lost);
        var retiredCount = resourcesList.Count(r => r.Status == Domain.Enums.ResourceStatus.Retired);
        
        var resourcesByType = resourcesList
            .GroupBy(r => r.Type)
            .ToDictionary(g => g.Key, g => g.Count());
        
        return new ResourceInventoryReportDto(
            centre.Id,
            centre.Name,
            resourcesList.Count,
            availableCount,
            allocatedCount,
            underRepairCount,
            lostCount,
            retiredCount,
            resourcesByType
        );
    }
}
