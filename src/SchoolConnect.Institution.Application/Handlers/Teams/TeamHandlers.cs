using MediatR;
using SchoolConnect.Institution.Application.Commands.Teams;
using SchoolConnect.Institution.Application.Queries.Teams;
using SchoolConnect.Institution.Domain.DTOs;
using SchoolConnect.Institution.Domain.Entities;
using SchoolConnect.Institution.Domain.Interfaces;

namespace SchoolConnect.Institution.Application.Handlers.Teams;

// Command Handlers
public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, TeamDto>
{
    private readonly ITeamRepository _repository;
    
    public CreateTeamHandler(ITeamRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<TeamDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = Team.Create(
            request.InstituteId,
            request.Name,
            request.Type,
            request.CentreId,
            request.Description,
            request.LeaderId
        );
        
        await _repository.AddAsync(team);
        
        return MapToDto(team);
    }
    
    private static TeamDto MapToDto(Team team) => new(
        team.Id,
        team.InstituteId,
        team.CentreId,
        team.Name,
        team.Description,
        team.Type,
        team.LeaderId,
        team.CreatedAt,
        team.UpdatedAt
    );
}

public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, TeamDto>
{
    private readonly ITeamRepository _repository;
    
    public UpdateTeamHandler(ITeamRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<TeamDto> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _repository.GetByIdAsync(request.Id);
        if (team == null) throw new Exception($"Team not found: {request.Id}");
        
        team.Update(request.Name, request.Description, request.LeaderId);
        
        await _repository.UpdateAsync(team);
        
        return MapToDto(team);
    }
    
    private static TeamDto MapToDto(Team team) => new(
        team.Id,
        team.InstituteId,
        team.CentreId,
        team.Name,
        team.Description,
        team.Type,
        team.LeaderId,
        team.CreatedAt,
        team.UpdatedAt
    );
}

public class DeleteTeamHandler : IRequestHandler<DeleteTeamCommand, bool>
{
    private readonly ITeamRepository _repository;
    
    public DeleteTeamHandler(ITeamRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _repository.GetByIdAsync(request.Id);
        if (team == null) return false;
        
        team.Delete();
        
        await _repository.DeleteAsync(request.Id);
        
        return true;
    }
}

public class AddTeamMemberHandler : IRequestHandler<AddTeamMemberCommand, TeamMemberDto>
{
    private readonly ITeamMemberRepository _memberRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IStaffRepository _staffRepository;
    
    public AddTeamMemberHandler(
        ITeamMemberRepository memberRepository,
        ITeamRepository teamRepository,
        IStaffRepository staffRepository)
    {
        _memberRepository = memberRepository;
        _teamRepository = teamRepository;
        _staffRepository = staffRepository;
    }
    
    public async Task<TeamMemberDto> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetByIdAsync(request.TeamId);
        if (team == null) throw new Exception($"Team not found: {request.TeamId}");
        
        var staff = await _staffRepository.GetByIdAsync(request.StaffMemberId);
        if (staff == null) throw new Exception($"Staff member not found: {request.StaffMemberId}");
        
        // Check if already a member
        var existingMember = await _memberRepository.GetActiveTeamMemberAsync(request.TeamId, request.StaffMemberId);
        if (existingMember != null)
        {
            throw new Exception("Staff member is already a member of this team");
        }
        
        var member = TeamMember.Create(
            request.TeamId,
            request.StaffMemberId,
            request.Role
        );
        
        await _memberRepository.AddAsync(member);
        
        return new TeamMemberDto(
            member.Id,
            member.TeamId,
            member.StaffMemberId,
            $"{staff.FirstName} {staff.LastName}",
            member.Role,
            member.JoinedAt,
            member.LeftAt
        );
    }
}

public class RemoveTeamMemberHandler : IRequestHandler<RemoveTeamMemberCommand, bool>
{
    private readonly ITeamMemberRepository _memberRepository;
    
    public RemoveTeamMemberHandler(ITeamMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }
    
    public async Task<bool> Handle(RemoveTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId);
        if (member == null) return false;
        
        if (member.TeamId != request.TeamId) return false;
        
        member.Remove();
        
        await _memberRepository.UpdateAsync(member);
        
        return true;
    }
}

// Query Handlers
public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, TeamDto?>
{
    private readonly ITeamRepository _repository;
    
    public GetTeamByIdHandler(ITeamRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<TeamDto?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await _repository.GetByIdAsync(request.Id);
        if (team == null) return null;
        
        return MapToDto(team);
    }
    
    private static TeamDto MapToDto(Team team) => new(
        team.Id,
        team.InstituteId,
        team.CentreId,
        team.Name,
        team.Description,
        team.Type,
        team.LeaderId,
        team.CreatedAt,
        team.UpdatedAt
    );
}

public class GetTeamsByInstituteHandler : IRequestHandler<GetTeamsByInstituteQuery, IEnumerable<TeamDto>>
{
    private readonly ITeamRepository _repository;
    
    public GetTeamsByInstituteHandler(ITeamRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<TeamDto>> Handle(GetTeamsByInstituteQuery request, CancellationToken cancellationToken)
    {
        var teams = await _repository.GetByInstituteIdAsync(request.InstituteId);
        return teams.Select(MapToDto);
    }
    
    private static TeamDto MapToDto(Team team) => new(
        team.Id,
        team.InstituteId,
        team.CentreId,
        team.Name,
        team.Description,
        team.Type,
        team.LeaderId,
        team.CreatedAt,
        team.UpdatedAt
    );
}

public class GetTeamMembersHandler : IRequestHandler<GetTeamMembersQuery, IEnumerable<TeamMemberDto>>
{
    private readonly ITeamMemberRepository _memberRepository;
    private readonly IStaffRepository _staffRepository;
    
    public GetTeamMembersHandler(ITeamMemberRepository memberRepository, IStaffRepository staffRepository)
    {
        _memberRepository = memberRepository;
        _staffRepository = staffRepository;
    }
    
    public async Task<IEnumerable<TeamMemberDto>> Handle(GetTeamMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _memberRepository.GetByTeamIdAsync(request.TeamId);
        
        var memberDtos = new List<TeamMemberDto>();
        foreach (var member in members)
        {
            var staff = await _staffRepository.GetByIdAsync(member.StaffMemberId);
            if (staff != null)
            {
                memberDtos.Add(new TeamMemberDto(
                    member.Id,
                    member.TeamId,
                    member.StaffMemberId,
                    $"{staff.FirstName} {staff.LastName}",
                    member.Role,
                    member.JoinedAt,
                    member.LeftAt
                ));
            }
        }
        
        return memberDtos;
    }
}
