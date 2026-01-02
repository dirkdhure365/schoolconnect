using AutoMapper;
using SchoolConnect.Collaboration.Application.DTOs;
using SchoolConnect.Collaboration.Domain.Entities;
using SchoolConnect.Collaboration.Domain.ValueObjects;

namespace SchoolConnect.Collaboration.Application.Mappers;

public class CollaborationMappingProfile : Profile
{
    public CollaborationMappingProfile()
    {
        // Workspace mappings
        CreateMap<Workspace, WorkspaceDto>();
        CreateMap<Workspace, WorkspaceSummaryDto>();
        CreateMap<WorkspaceMember, WorkspaceMemberDto>();

        // Board mappings
        CreateMap<Board, BoardDto>()
            .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Background));
        CreateMap<Board, BoardSummaryDto>();
        CreateMap<BoardBackground, BoardBackgroundDto>();

        // List mappings
        CreateMap<BoardList, ListDto>();

        // Card mappings
        CreateMap<Card, CardDto>();
        CreateMap<Card, CardSummaryDto>();
        CreateMap<AssigneeInfo, AssigneeInfoDto>();
        CreateMap<CardCover, CardCoverDto>();
        CreateMap<CardAttachment, CardAttachmentDto>();

        // Checklist mappings
        CreateMap<CardChecklist, ChecklistDto>();
        CreateMap<ChecklistItem, ChecklistItemDto>();

        // Comment mappings
        CreateMap<CardComment, CardCommentDto>();

        // Label mappings
        CreateMap<CardLabel, CardLabelDto>();

        // Activity mappings
        CreateMap<CardActivity, CardActivityDto>();

        // Shared resource mappings
        CreateMap<SharedResource, SharedResourceDto>();
    }
}
