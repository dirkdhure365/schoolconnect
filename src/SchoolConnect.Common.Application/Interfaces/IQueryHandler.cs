using MediatR;

namespace SchoolConnect.Common.Application.Interfaces;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
{
}
