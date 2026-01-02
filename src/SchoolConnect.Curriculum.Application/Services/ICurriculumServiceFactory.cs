namespace SchoolConnect.Curriculum.Application.Services;

/// <summary>
/// Factory for creating curriculum service instances based on board code.
/// </summary>
public interface ICurriculumServiceFactory
{
    /// <summary>
    /// Creates a curriculum service for the specified board.
    /// </summary>
    /// <param name="boardCode">Board code (e.g., "CAPS", "ZIMSEC").</param>
    /// <returns>Curriculum service instance.</returns>
    ICurriculumService CreateService(string boardCode);

    /// <summary>
    /// Gets all registered board codes.
    /// </summary>
    IEnumerable<string> GetRegisteredBoards();

    /// <summary>
    /// Gets board information.
    /// </summary>
    Task<IEnumerable<BoardInfo>> GetBoardsAsync();

    /// <summary>
    /// Gets boards for a specific country.
    /// </summary>
    Task<IEnumerable<BoardInfo>> GetBoardsByCountryAsync(string country);
}

/// <summary>
/// Information about a curriculum board.
/// </summary>
public class BoardInfo
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ExaminationBoard { get; set; } = string.Empty;
    public bool SupportsPracticalAssessments { get; set; }
}
