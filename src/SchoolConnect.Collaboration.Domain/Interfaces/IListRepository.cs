using SchoolConnect.Collaboration.Domain.Entities;

namespace SchoolConnect.Collaboration.Domain.Interfaces;

public interface IListRepository
{
    Task<BoardList?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<BoardList>> GetByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default);
    Task<List<BoardList>> GetArchivedByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default);
    Task AddAsync(BoardList list, CancellationToken cancellationToken = default);
    Task UpdateAsync(BoardList list, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
