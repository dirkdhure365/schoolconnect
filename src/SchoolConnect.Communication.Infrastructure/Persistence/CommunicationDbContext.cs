using MongoDB.Driver;
using SchoolConnect.Communication.Domain.Entities;

namespace SchoolConnect.Communication.Infrastructure.Persistence;

public class CommunicationDbContext
{
    private readonly IMongoDatabase _database;

    public CommunicationDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<Message> Messages => 
        _database.GetCollection<Message>("messages");
    
    public IMongoCollection<Conversation> Conversations => 
        _database.GetCollection<Conversation>("conversations");
    
    public IMongoCollection<Notification> Notifications => 
        _database.GetCollection<Notification>("notifications");
    
    public IMongoCollection<NotificationPreference> NotificationPreferences => 
        _database.GetCollection<NotificationPreference>("notification_preferences");
    
    public IMongoCollection<Announcement> Announcements => 
        _database.GetCollection<Announcement>("announcements");
    
    public IMongoCollection<AnnouncementAcknowledgment> AnnouncementAcknowledgments => 
        _database.GetCollection<AnnouncementAcknowledgment>("announcement_acknowledgments");
    
    public IMongoCollection<FeedItem> FeedItems => 
        _database.GetCollection<FeedItem>("feed_items");
}
