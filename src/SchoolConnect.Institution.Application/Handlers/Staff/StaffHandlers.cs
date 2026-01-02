using MediatR;
using SchoolConnect.Institution.Application.Commands.Staff;
using SchoolConnect.Institution.Application.Queries.Staff;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;

namespace SchoolConnect.Institution.Application.Handlers.Staff;

// Command Handlers
public class OnboardStaffHandler : IRequestHandler<OnboardStaffCommand, StaffMemberDto>
{
    private readonly IStaffRepository _repository;
    
    public OnboardStaffHandler(IStaffRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<StaffMemberDto> Handle(OnboardStaffCommand request, CancellationToken cancellationToken)
    {
        var staff = StaffMember.Create(
            request.InstituteId,
            request.UserId,
            request.EmployeeCode,
            request.FirstName,
            request.LastName,
            request.EmploymentType,
            request.JoinDate,
            request.JobTitle,
            request.Department,
            request.Qualifications,
            request.Specializations,
            request.MaxTeachingHoursPerWeek
        );
        
        await _repository.AddAsync(staff);
        
        return MapToDto(staff);
    }
    
    private static StaffMemberDto MapToDto(StaffMember staff) => new(
        staff.Id,
        staff.InstituteId,
        staff.UserId,
        staff.EmployeeCode,
        staff.FirstName,
        staff.LastName,
        staff.JobTitle,
        staff.Department,
        staff.EmploymentType,
        staff.JoinDate,
        staff.TerminationDate,
        staff.Status,
        staff.Qualifications,
        staff.Specializations,
        staff.MaxTeachingHoursPerWeek,
        staff.CreatedAt,
        staff.UpdatedAt
    );
}

public class UpdateStaffHandler : IRequestHandler<UpdateStaffCommand, StaffMemberDto>
{
    private readonly IStaffRepository _repository;
    
    public UpdateStaffHandler(IStaffRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<StaffMemberDto> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
    {
        var staff = await _repository.GetByIdAsync(request.Id);
        if (staff == null) throw new Exception($"Staff member not found: {request.Id}");
        
        staff.Update(request.FirstName, request.LastName, request.JobTitle, request.Department, request.MaxTeachingHoursPerWeek);
        
        await _repository.UpdateAsync(staff);
        
        return MapToDto(staff);
    }
    
    private static StaffMemberDto MapToDto(StaffMember staff) => new(
        staff.Id,
        staff.InstituteId,
        staff.UserId,
        staff.EmployeeCode,
        staff.FirstName,
        staff.LastName,
        staff.JobTitle,
        staff.Department,
        staff.EmploymentType,
        staff.JoinDate,
        staff.TerminationDate,
        staff.Status,
        staff.Qualifications,
        staff.Specializations,
        staff.MaxTeachingHoursPerWeek,
        staff.CreatedAt,
        staff.UpdatedAt
    );
}

public class OffboardStaffHandler : IRequestHandler<OffboardStaffCommand, bool>
{
    private readonly IStaffRepository _repository;
    
    public OffboardStaffHandler(IStaffRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<bool> Handle(OffboardStaffCommand request, CancellationToken cancellationToken)
    {
        var staff = await _repository.GetByIdAsync(request.Id);
        if (staff == null) return false;
        
        staff.Terminate(request.TerminationDate);
        
        await _repository.UpdateAsync(staff);
        
        return true;
    }
}

public class AssignStaffToCentreHandler : IRequestHandler<AssignStaffToCentreCommand, bool>
{
    private readonly IStaffCentreAssignmentRepository _assignmentRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICentreRepository _centreRepository;
    
    public AssignStaffToCentreHandler(
        IStaffCentreAssignmentRepository assignmentRepository,
        IStaffRepository staffRepository,
        ICentreRepository centreRepository)
    {
        _assignmentRepository = assignmentRepository;
        _staffRepository = staffRepository;
        _centreRepository = centreRepository;
    }
    
    public async Task<bool> Handle(AssignStaffToCentreCommand request, CancellationToken cancellationToken)
    {
        var staff = await _staffRepository.GetByIdAsync(request.StaffMemberId);
        if (staff == null) throw new Exception($"Staff member not found: {request.StaffMemberId}");
        
        var centre = await _centreRepository.GetByIdAsync(request.CentreId);
        if (centre == null) throw new Exception($"Centre not found: {request.CentreId}");
        
        // Check if already assigned
        var existingAssignment = await _assignmentRepository.GetActiveAssignmentAsync(request.StaffMemberId, request.CentreId);
        if (existingAssignment != null)
        {
            throw new Exception("Staff member is already assigned to this centre");
        }
        
        var assignment = StaffCentreAssignment.Create(
            request.StaffMemberId,
            request.CentreId,
            request.AssignedBy,
            request.StartDate,
            request.IsPrimary
        );
        
        await _assignmentRepository.AddAsync(assignment);
        
        return true;
    }
}

public class RemoveStaffFromCentreHandler : IRequestHandler<RemoveStaffFromCentreCommand, bool>
{
    private readonly IStaffCentreAssignmentRepository _assignmentRepository;
    
    public RemoveStaffFromCentreHandler(IStaffCentreAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }
    
    public async Task<bool> Handle(RemoveStaffFromCentreCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _assignmentRepository.GetActiveAssignmentAsync(request.StaffMemberId, request.CentreId);
        if (assignment == null) return false;
        
        assignment.Remove(request.EndDate);
        
        await _assignmentRepository.UpdateAsync(assignment);
        
        return true;
    }
}

// Query Handlers
public class GetStaffByIdHandler : IRequestHandler<GetStaffByIdQuery, StaffMemberDto?>
{
    private readonly IStaffRepository _repository;
    
    public GetStaffByIdHandler(IStaffRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<StaffMemberDto?> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
    {
        var staff = await _repository.GetByIdAsync(request.Id);
        if (staff == null) return null;
        
        return MapToDto(staff);
    }
    
    private static StaffMemberDto MapToDto(StaffMember staff) => new(
        staff.Id,
        staff.InstituteId,
        staff.UserId,
        staff.EmployeeCode,
        staff.FirstName,
        staff.LastName,
        staff.JobTitle,
        staff.Department,
        staff.EmploymentType,
        staff.JoinDate,
        staff.TerminationDate,
        staff.Status,
        staff.Qualifications,
        staff.Specializations,
        staff.MaxTeachingHoursPerWeek,
        staff.CreatedAt,
        staff.UpdatedAt
    );
}

public class GetStaffByInstituteHandler : IRequestHandler<GetStaffByInstituteQuery, IEnumerable<StaffMemberDto>>
{
    private readonly IStaffRepository _repository;
    
    public GetStaffByInstituteHandler(IStaffRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<StaffMemberDto>> Handle(GetStaffByInstituteQuery request, CancellationToken cancellationToken)
    {
        var staffMembers = await _repository.GetByInstituteIdAsync(request.InstituteId);
        return staffMembers.Select(MapToDto);
    }
    
    private static StaffMemberDto MapToDto(StaffMember staff) => new(
        staff.Id,
        staff.InstituteId,
        staff.UserId,
        staff.EmployeeCode,
        staff.FirstName,
        staff.LastName,
        staff.JobTitle,
        staff.Department,
        staff.EmploymentType,
        staff.JoinDate,
        staff.TerminationDate,
        staff.Status,
        staff.Qualifications,
        staff.Specializations,
        staff.MaxTeachingHoursPerWeek,
        staff.CreatedAt,
        staff.UpdatedAt
    );
}

public class GetStaffByCentreHandler : IRequestHandler<GetStaffByCentreQuery, IEnumerable<StaffMemberDto>>
{
    private readonly IStaffRepository _staffRepository;
    private readonly IStaffCentreAssignmentRepository _assignmentRepository;
    
    public GetStaffByCentreHandler(IStaffRepository staffRepository, IStaffCentreAssignmentRepository assignmentRepository)
    {
        _staffRepository = staffRepository;
        _assignmentRepository = assignmentRepository;
    }
    
    public async Task<IEnumerable<StaffMemberDto>> Handle(GetStaffByCentreQuery request, CancellationToken cancellationToken)
    {
        var assignments = await _assignmentRepository.GetByCentreIdAsync(request.CentreId);
        var activeAssignments = assignments.Where(a => a.EndDate == null).ToList();
        
        var staffMembers = new List<StaffMemberDto>();
        foreach (var assignment in activeAssignments)
        {
            var staff = await _staffRepository.GetByIdAsync(assignment.StaffMemberId);
            if (staff != null)
            {
                staffMembers.Add(MapToDto(staff));
            }
        }
        
        return staffMembers;
    }
    
    private static StaffMemberDto MapToDto(StaffMember staff) => new(
        staff.Id,
        staff.InstituteId,
        staff.UserId,
        staff.EmployeeCode,
        staff.FirstName,
        staff.LastName,
        staff.JobTitle,
        staff.Department,
        staff.EmploymentType,
        staff.JoinDate,
        staff.TerminationDate,
        staff.Status,
        staff.Qualifications,
        staff.Specializations,
        staff.MaxTeachingHoursPerWeek,
        staff.CreatedAt,
        staff.UpdatedAt
    );
}

public class GetStaffCentresHandler : IRequestHandler<GetStaffCentresQuery, IEnumerable<CentreSummaryDto>>
{
    private readonly IStaffCentreAssignmentRepository _assignmentRepository;
    private readonly ICentreRepository _centreRepository;
    
    public GetStaffCentresHandler(IStaffCentreAssignmentRepository assignmentRepository, ICentreRepository centreRepository)
    {
        _assignmentRepository = assignmentRepository;
        _centreRepository = centreRepository;
    }
    
    public async Task<IEnumerable<CentreSummaryDto>> Handle(GetStaffCentresQuery request, CancellationToken cancellationToken)
    {
        var assignments = await _assignmentRepository.GetByStaffIdAsync(request.StaffId);
        var activeAssignments = assignments.Where(a => a.EndDate == null).ToList();
        
        var centres = new List<CentreSummaryDto>();
        foreach (var assignment in activeAssignments)
        {
            var centre = await _centreRepository.GetByIdAsync(assignment.CentreId);
            if (centre != null)
            {
                centres.Add(new CentreSummaryDto(
                    centre.Id,
                    centre.Name,
                    centre.Code,
                    centre.Status,
                    centre.Capacity
                ));
            }
        }
        
        return centres;
    }
}

public class GetStaffTeamsHandler : IRequestHandler<GetStaffTeamsQuery, IEnumerable<TeamDto>>
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ITeamRepository _teamRepository;
    
    public GetStaffTeamsHandler(ITeamMemberRepository teamMemberRepository, ITeamRepository teamRepository)
    {
        _teamMemberRepository = teamMemberRepository;
        _teamRepository = teamRepository;
    }
    
    public async Task<IEnumerable<TeamDto>> Handle(GetStaffTeamsQuery request, CancellationToken cancellationToken)
    {
        var memberships = await _teamMemberRepository.GetByStaffIdAsync(request.StaffId);
        var activeMemberships = memberships.Where(m => m.LeftAt == null).ToList();
        
        var teams = new List<TeamDto>();
        foreach (var membership in activeMemberships)
        {
            var team = await _teamRepository.GetByIdAsync(membership.TeamId);
            if (team != null)
            {
                teams.Add(new TeamDto(
                    team.Id,
                    team.InstituteId,
                    team.CentreId,
                    team.Name,
                    team.Description,
                    team.Type,
                    team.LeaderId,
                    team.CreatedAt,
                    team.UpdatedAt
                ));
            }
        }
        
        return teams;
    }
}
