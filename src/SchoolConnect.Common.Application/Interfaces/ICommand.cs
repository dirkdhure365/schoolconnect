using MediatR;
using SchoolConnect.Common.Application.Models;

namespace SchoolConnect.Common.Application.Interfaces;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
