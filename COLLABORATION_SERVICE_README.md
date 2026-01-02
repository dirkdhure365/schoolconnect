# SchoolConnect Collaboration Microservice

A Trello-style collaboration platform for the SchoolConnect education system, providing workspaces, boards, lists, and cards for team collaboration, lesson planning, and project management.

## Architecture

The service follows **Clean Architecture** principles with four distinct layers:

```
SchoolConnect.Collaboration/
├── Domain/          - Business entities, value objects, and domain logic
├── Application/     - Use cases, DTOs, commands, queries, and handlers
├── Infrastructure/  - Data access, repositories, and external services
└── Api/            - HTTP endpoints and API configuration
```

## Technology Stack

- **.NET 10.0** - Latest .NET framework
- **MongoDB** - Document database for flexible schema
- **MediatR** - CQRS pattern implementation
- **AutoMapper** - Object-to-object mapping
- **ASP.NET Core Minimal APIs** - Lightweight HTTP endpoints
- **Swagger/OpenAPI** - API documentation

## Domain Model

### Core Entities

#### Workspace
Multi-tenant collaboration space for organizing boards and teams.
- Institute/Centre scoping
- Member management with roles (Owner, Admin, Member, Guest)
- Customizable visibility (Private, Institute, Centre, Public)
- Settings for invites, guest access, and defaults

#### Board
Kanban-style board for organizing lists and cards.
- Customizable backgrounds (Color, Image, Gradient)
- Board templates and cloning
- Archive/restore functionality
- Starring for quick access

#### List (BoardList)
Container for organizing cards with optional WIP limits.
- Positioned containers
- Work-in-progress (WIP) limits
- Archive capability
- Color coding

#### Card
Feature-rich task/item with comprehensive metadata.
- Title, description, position
- Multiple assignees
- Labels and priority levels
- Due dates and start dates
- Cover images
- Attachments
- Checklists with progress tracking
- Comments with mentions
- Watchers for notifications
- Activity tracking

### Supporting Entities

- **WorkspaceMember** - User membership in workspace with roles
- **CardLabel** - Reusable labels for categorization
- **CardChecklist** - Task lists within cards with completion tracking
- **ChecklistItem** - Individual checklist tasks
- **CardComment** - Threaded comments with mentions
- **CardActivity** - Audit trail of all card changes
- **SharedResource** - Shared files and links

## API Endpoints

### Workspaces
```
GET    /api/workspaces/{id}              - Get workspace by ID
POST   /api/workspaces                   - Create workspace
PUT    /api/workspaces/{id}              - Update workspace
DELETE /api/workspaces/{id}              - Delete workspace
POST   /api/workspaces/{id}/members      - Add member to workspace
```

### Boards
```
GET    /api/boards/{id}                         - Get board by ID
POST   /api/boards                              - Create board
PUT    /api/boards/{id}                         - Update board
GET    /api/workspaces/{workspaceId}/boards    - Get workspace boards
```

### Lists
```
POST   /api/lists                        - Create list
PUT    /api/lists/{id}                   - Update list
GET    /api/boards/{boardId}/lists       - Get board lists
```

### Cards
```
GET    /api/cards/{id}                   - Get card by ID
POST   /api/cards                        - Create card
PUT    /api/cards/{id}                   - Update card
PUT    /api/cards/{id}/move              - Move card to different list
GET    /api/lists/{listId}/cards         - Get list cards
```

## Domain Events

The service emits 30+ domain events for real-time updates and integration:

### Workspace Events
- `WorkspaceCreatedEvent`
- `WorkspaceUpdatedEvent`
- `WorkspaceDeletedEvent`
- `WorkspaceMemberAddedEvent`
- `WorkspaceMemberRemovedEvent`
- `WorkspaceMemberRoleChangedEvent`

### Board Events
- `BoardCreatedEvent`
- `BoardUpdatedEvent`
- `BoardArchivedEvent`
- `BoardDeletedEvent`

### List Events
- `ListCreatedEvent`
- `ListUpdatedEvent`
- `ListArchivedEvent`
- `ListMovedEvent`

### Card Events
- `CardCreatedEvent`
- `CardUpdatedEvent`
- `CardMovedEvent`
- `CardArchivedEvent`
- `CardDeletedEvent`
- `CardAssignedEvent`
- `CardUnassignedEvent`
- `CardLabelAddedEvent`
- `CardLabelRemovedEvent`
- `CardDueDateSetEvent`
- `CardDueDateApproachingEvent`
- `CardCommentAddedEvent`
- `CardCommentUpdatedEvent`
- `CardCommentDeletedEvent`
- `ChecklistCreatedEvent`
- `ChecklistItemCompletedEvent`

### Resource Events
- `ResourceSharedEvent`

## Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017"
  },
  "MongoDB": {
    "DatabaseName": "schoolconnect_collaboration"
  }
}
```

### Environment Variables
- `ConnectionStrings__MongoDB` - MongoDB connection string
- `MongoDB__DatabaseName` - Database name (default: schoolconnect_collaboration)

## Running the Service

### Prerequisites
- .NET 10.0 SDK
- MongoDB 5.0+

### Development
```bash
cd src/SchoolConnect.Collaboration.Api
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger UI: https://localhost:5001/swagger

### Production
```bash
dotnet publish -c Release
cd bin/Release/net10.0/publish
dotnet SchoolConnect.Collaboration.Api.dll
```

## Database Collections

MongoDB collections created by the service:

- `workspaces` - Workspace documents
- `workspace_members` - Workspace membership
- `boards` - Board documents
- `lists` - List documents
- `cards` - Card documents
- `card_labels` - Label definitions
- `card_checklists` - Checklist documents
- `card_comments` - Comment documents
- `card_activities` - Activity audit trail
- `shared_resources` - Shared file/link metadata

## Use Cases

### Education Scenarios

1. **Lesson Planning**
   - Create workspace for department
   - Board per term/subject
   - Lists for weeks
   - Cards for individual lessons
   - Checklists for lesson components
   - Attachments for resources

2. **Project-Based Learning**
   - Student group workspaces
   - Project boards
   - Task assignment to students
   - Progress tracking via checklists
   - File sharing and collaboration

3. **Curriculum Development**
   - Subject committee workspaces
   - Boards for curriculum units
   - Card-based learning objectives
   - Resource attachment
   - Collaborative commenting

4. **Event Planning**
   - School event workspaces
   - Planning boards
   - Task delegation
   - Timeline via due dates
   - Activity tracking

## Future Enhancements

Planned features for future releases:

- [ ] Real-time collaboration via SignalR
- [ ] Board templates library
- [ ] Power-up integrations
- [ ] Advanced search and filtering
- [ ] Calendar view for due dates
- [ ] Gantt chart visualization
- [ ] Email notifications
- [ ] Mobile app support
- [ ] Bulk operations
- [ ] Custom fields
- [ ] Automation rules
- [ ] Export to PDF/Excel
- [ ] Webhooks for integrations
- [ ] Advanced analytics and reporting

## Integration Points

The Collaboration service integrates with other SchoolConnect services:

- **Identity Service** - User authentication and authorization
- **Institution Service** - Institute and centre data
- **Communication Service** - Notifications and mentions
- **Curriculum Service** - Curriculum-linked lesson planning

## Development Guidelines

### Adding New Commands

1. Create command record in `Application/Commands/`
2. Implement handler in `Application/Handlers/`
3. Add endpoint in `Api/Endpoints/`

### Adding New Queries

1. Create query record in `Application/Queries/`
2. Implement handler in `Application/Handlers/`
3. Add endpoint in `Api/Endpoints/`

### Adding New Entities

1. Create entity in `Domain/Entities/`
2. Add to `CollaborationDbContext`
3. Create repository interface in `Domain/Interfaces/`
4. Implement repository in `Infrastructure/Repositories/`
5. Register in `ServiceCollectionExtensions`
6. Create DTOs in `Application/DTOs/`
7. Add AutoMapper mappings

## License

Copyright © 2026 SchoolConnect. All rights reserved.
