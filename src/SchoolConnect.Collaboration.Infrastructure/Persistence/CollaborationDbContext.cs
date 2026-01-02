using MongoDB.Driver;
using SchoolConnect.Collaboration.Domain.Entities;

namespace SchoolConnect.Collaboration.Infrastructure.Persistence;

public class CollaborationDbContext
{
    private readonly IMongoDatabase _database;

    public CollaborationDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<Workspace> Workspaces => 
        _database.GetCollection<Workspace>("workspaces");
    
    public IMongoCollection<WorkspaceMember> WorkspaceMembers => 
        _database.GetCollection<WorkspaceMember>("workspace_members");
    
    public IMongoCollection<Board> Boards => 
        _database.GetCollection<Board>("boards");
    
    public IMongoCollection<BoardList> Lists => 
        _database.GetCollection<BoardList>("lists");
    
    public IMongoCollection<Card> Cards => 
        _database.GetCollection<Card>("cards");
    
    public IMongoCollection<CardLabel> CardLabels => 
        _database.GetCollection<CardLabel>("card_labels");
    
    public IMongoCollection<CardChecklist> CardChecklists => 
        _database.GetCollection<CardChecklist>("card_checklists");
    
    public IMongoCollection<CardComment> CardComments => 
        _database.GetCollection<CardComment>("card_comments");
    
    public IMongoCollection<CardActivity> CardActivities => 
        _database.GetCollection<CardActivity>("card_activities");
    
    public IMongoCollection<SharedResource> SharedResources => 
        _database.GetCollection<SharedResource>("shared_resources");
}
