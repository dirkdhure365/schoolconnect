using MediatR;
using SchoolConnect.Common.Application.Models;

namespace SchoolConnect.Common.Application.Interfaces;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result> 
    where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> 
    where TCommand : ICommand<TResponse>
{
}
