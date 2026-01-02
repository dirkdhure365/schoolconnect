using MediatR;
using Microsoft.Extensions.Logging;
using SchoolConnect.Common.Domain.Interfaces;

namespace SchoolConnect.Common.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IUnitOfWork? _unitOfWork;

    public TransactionBehavior(
        ILogger<TransactionBehavior<TRequest, TResponse>> logger,
        IUnitOfWork? unitOfWork = null)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_unitOfWork == null)
        {
            return await next();
        }

        var requestName = typeof(TRequest).Name;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            
            _logger.LogInformation("Begin transaction for {RequestName}", requestName);

            var response = await next();

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            
            _logger.LogInformation("Committed transaction for {RequestName}", requestName);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction rollback for {RequestName}", requestName);
            
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            
            throw;
        }
    }
}
