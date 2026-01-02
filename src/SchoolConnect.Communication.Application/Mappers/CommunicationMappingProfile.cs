using AutoMapper;
using SchoolConnect.Communication.Application.DTOs;
using SchoolConnect.Communication.Domain.Entities;
using SchoolConnect.Communication.Domain.ValueObjects;

namespace SchoolConnect.Communication.Application.Mappers;

public class CommunicationMappingProfile : Profile
{
    public CommunicationMappingProfile()
    {
        // Message mappings
        CreateMap<Message, MessageDto>();
        CreateMap<MessageAttachment, MessageAttachmentDto>();
        
        // Conversation mappings
        CreateMap<Conversation, ConversationDto>();
        CreateMap<Conversation, ConversationSummaryDto>()
            .ForMember(dest => dest.UnreadCount, opt => opt.MapFrom(src => 0)); // Will be calculated separately
        CreateMap<ConversationParticipant, ParticipantDto>();
        
        // Notification mappings
        CreateMap<Notification, NotificationDto>();
        CreateMap<NotificationPreference, NotificationPreferenceDto>();
        
        // Announcement mappings
        CreateMap<Announcement, AnnouncementDto>();
        CreateMap<Announcement, AnnouncementSummaryDto>();
        CreateMap<AnnouncementAcknowledgment, AcknowledgmentDto>();
        
        // Feed mappings
        CreateMap<FeedItem, FeedItemDto>();
    }
}
