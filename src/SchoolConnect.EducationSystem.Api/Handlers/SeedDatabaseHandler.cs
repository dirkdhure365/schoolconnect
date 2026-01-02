using MediatR;
using SchoolConnect.EducationSystem.Application.Commands;
using SchoolConnect.EducationSystem.Infrastructure.Seed;

namespace SchoolConnect.EducationSystem.Api.Handlers;

public class SeedDatabaseHandler : IRequestHandler<SeedDatabaseCommand, bool>
{
    private readonly DataSeeder _seeder;

    public SeedDatabaseHandler(DataSeeder seeder)
    {
        _seeder = seeder;
    }

    public async Task<bool> Handle(SeedDatabaseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _seeder.SeedDataAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
