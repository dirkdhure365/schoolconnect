namespace SchoolConnect.Curriculum.Application.Services;

/// <summary>
/// Factory implementation for creating curriculum services.
/// </summary>
public class CurriculumServiceFactory : ICurriculumServiceFactory
{
    private readonly Dictionary<string, Func<ICurriculumService>> _serviceFactories = new();
    private readonly Dictionary<string, BoardInfo> _boardInfos = new();

    /// <summary>
    /// Registers a curriculum service for a board.
    /// </summary>
    public void RegisterService(string boardCode, Func<ICurriculumService> factory, BoardInfo boardInfo)
    {
        _serviceFactories[boardCode.ToUpperInvariant()] = factory;
        _boardInfos[boardCode.ToUpperInvariant()] = boardInfo;
    }

    public ICurriculumService CreateService(string boardCode)
    {
        var key = boardCode.ToUpperInvariant();
        if (!_serviceFactories.TryGetValue(key, out var factory))
        {
            throw new ArgumentException($"No curriculum service registered for board code: {boardCode}", nameof(boardCode));
        }

        return factory();
    }

    public IEnumerable<string> GetRegisteredBoards()
    {
        return _serviceFactories.Keys;
    }

    public Task<IEnumerable<BoardInfo>> GetBoardsAsync()
    {
        return Task.FromResult<IEnumerable<BoardInfo>>(_boardInfos.Values);
    }

    public Task<IEnumerable<BoardInfo>> GetBoardsByCountryAsync(string country)
    {
        var boards = _boardInfos.Values
            .Where(b => b.Country.Equals(country, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(boards);
    }
}
