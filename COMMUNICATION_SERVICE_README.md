# SchoolConnect Communication Microservice

## Overview

The Communication microservice handles all messaging, notifications, announcements, and activity feeds for the SchoolConnect platform. It provides real-time and asynchronous communication capabilities for all stakeholders including students, parents, teachers, and administrators.

## Architecture

The microservice follows **Clean Architecture** principles with clear separation of concerns:

```
SchoolConnect.Communication/
├── Domain/          # Core business logic and entities
├── Application/     # Use cases, commands, queries, DTOs
├── Infrastructure/  # Data access, external services
└── Api/            # REST API endpoints
```

## Features

### 1. **Messaging**
- Direct conversations (1-on-1)
- Group conversations
- Class-based conversations
- Broadcast messages
- Message attachments
- Reply and forward functionality
- Read receipts
- Message priority levels

### 2. **Notifications**
- Multi-channel delivery (In-App, Push, Email, SMS)
- User notification preferences
- Quiet hours configuration
- Email digest options
- Notification types: System, Message, Announcement, Homework, Assessment, Grade, Attendance, Payment, Calendar, Reminder, Alert
- Expiration support

### 3. **Announcements**
- Institute-wide and centre-specific announcements
- Targeted audience filtering
- Priority levels (Low, Normal, High, Critical)
- Pin/unpin functionality
- Scheduled publishing
- Acknowledgment tracking
- View count and reach analytics
- Attachment support

### 4. **Activity Feed**
- Personalized activity streams
- Feed item prioritization
- Read/unread tracking
- Feed preferences
- Auto-aggregation from various sources

## Domain Model

### Key Entities

#### Message
- Content and attachments
- Priority levels
- Reply/forward support
- Read receipts
- Soft delete capability

#### Conversation
- Multiple types (Direct, Group, Class, Broadcast)
- Participant management
- Last message tracking
- Archive functionality

#### Notification
- Multi-channel delivery
- Status tracking
- Action links
- Expiration dates
- Source tracking

#### Announcement
- Targeted audience with filters
- Priority and pinning
- Acknowledgment requirements
- Analytics (views, acknowledgments)
- Scheduled publishing

#### FeedItem
- Type-based categorization
- Priority ordering
- Read/dismiss functionality
- Source linking

### Value Objects
- **MessageAttachment**: File metadata with thumbnails
- **NotificationData**: Structured metadata for notifications
- **AudienceFilter**: Complex filtering for announcements
- **DeliveryChannel**: Channel configuration

### Enums
- MessageStatus, MessagePriority, ConversationType
- NotificationType, NotificationChannel, NotificationStatus
- AnnouncementStatus, AnnouncementPriority, TargetAudience
- FeedItemType

## API Endpoints

### Messages & Conversations
```
GET    /api/messages/conversations                    # Get user's conversations
GET    /api/messages/conversations/{id}               # Get conversation details
GET    /api/messages/conversations/{id}/messages      # Get conversation messages
POST   /api/messages/send                             # Send a message
POST   /api/messages/{id}/read                        # Mark message as read
DELETE /api/messages/{id}                             # Delete message
GET    /api/messages/unread-count                     # Get unread count
```

### Notifications
```
GET    /api/notifications                             # Get user notifications
GET    /api/notifications/unread-count                # Get unread count
POST   /api/notifications/send                        # Send notification
POST   /api/notifications/{id}/read                   # Mark as read
POST   /api/notifications/read-all                    # Mark all as read
```

### Announcements
```
GET    /api/announcements/institutes/{instituteId}   # Get announcements
GET    /api/announcements/{id}                        # Get announcement details
POST   /api/announcements                             # Create announcement
POST   /api/announcements/{id}/publish                # Publish announcement
POST   /api/announcements/{id}/acknowledge            # Acknowledge announcement
```

### Feed
```
GET    /api/feed                                      # Get user's feed
```

## Technology Stack

- **.NET 10.0**: Latest .NET framework
- **MongoDB**: NoSQL database for flexible document storage
- **MediatR**: CQRS pattern implementation
- **AutoMapper**: Object-to-object mapping
- **FluentValidation**: Input validation (ready for implementation)

## Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017"
  },
  "MongoDB": {
    "DatabaseName": "schoolconnect_communication"
  }
}
```

### Environment Variables
- `MONGODB_CONNECTION_STRING`: MongoDB connection string
- `MONGODB_DATABASE_NAME`: Database name

## Data Model

### MongoDB Collections
- `messages`: Message documents
- `conversations`: Conversation documents with embedded participants
- `notifications`: Notification documents
- `notification_preferences`: User notification preferences
- `announcements`: Announcement documents
- `announcement_acknowledgments`: Acknowledgment tracking
- `feed_items`: Feed item documents

## Getting Started

### Prerequisites
- .NET 10.0 SDK
- MongoDB 4.4+
- Optional: SignalR for real-time features

### Running the Service

```bash
# Navigate to API project
cd src/SchoolConnect.Communication.Api

# Run the service
dotnet run
```

The API will be available at `https://localhost:5001` (or configured port).

### Running with Docker
```bash
# Build the image
docker build -t schoolconnect-communication .

# Run the container
docker run -p 5001:8080 \
  -e MongoDB__ConnectionString="mongodb://host.docker.internal:27017" \
  -e MongoDB__DatabaseName="schoolconnect_communication" \
  schoolconnect-communication
```

## Development

### Project Structure

```
SchoolConnect.Communication.Domain/
├── Entities/           # Domain entities
├── ValueObjects/       # Value objects
├── Events/            # Domain events
├── Enums/             # Enumerations
├── Exceptions/        # Custom exceptions
└── Interfaces/        # Repository interfaces

SchoolConnect.Communication.Application/
├── Commands/          # Write operations
├── Queries/           # Read operations
├── DTOs/              # Data transfer objects
├── Handlers/          # Command/Query handlers
├── Mappers/           # AutoMapper profiles
└── IntegrationEvents/ # Integration event definitions

SchoolConnect.Communication.Infrastructure/
├── Persistence/       # Database context and configurations
├── Repositories/      # Repository implementations
├── Services/          # External service implementations
└── Extensions/        # DI extensions

SchoolConnect.Communication.Api/
├── Endpoints/         # Minimal API endpoints
└── Program.cs         # Application startup
```

### Adding New Features

1. **Define Domain Model**: Add entities, value objects, events in Domain layer
2. **Create Commands/Queries**: Add application-level operations in Application layer
3. **Implement Handlers**: Create handlers for commands and queries
4. **Add Repository Methods**: Extend repository interfaces and implementations
5. **Create Endpoints**: Map new endpoints in API layer
6. **Update DTOs**: Add/modify DTOs for data transfer

## Testing

### Unit Tests
```bash
dotnet test tests/SchoolConnect.Communication.Domain.Tests
dotnet test tests/SchoolConnect.Communication.Application.Tests
```

### Integration Tests
```bash
dotnet test tests/SchoolConnect.Communication.Integration.Tests
```

## Future Enhancements

- **SignalR Hubs**: Real-time message delivery (ChatHub, NotificationHub)
- **Push Notification Services**: Firebase, APNs integration
- **Email Service**: SMTP integration with templates
- **SMS Service**: Twilio/AWS SNS integration
- **Message Threading**: Nested conversation support
- **Rich Media**: Image/video inline support
- **Reactions**: Message reactions and emojis
- **Search**: Full-text search across messages
- **Analytics**: Advanced messaging analytics
- **Archiving**: Conversation and message archiving strategies

## Security Considerations

- Input validation on all endpoints
- Authorization checks for conversation access
- Rate limiting for message sending
- Content moderation capabilities
- Encrypted message storage (future)
- GDPR compliance for data retention

## Monitoring & Observability

- Health checks endpoint
- Structured logging
- Performance metrics
- Error tracking
- Database connection monitoring

## Contributing

Follow the existing code patterns and conventions:
- Use CQRS pattern for all operations
- Follow Clean Architecture layers
- Write unit tests for business logic
- Document public APIs
- Follow C# naming conventions

## License

Copyright © 2024 SchoolConnect Platform
