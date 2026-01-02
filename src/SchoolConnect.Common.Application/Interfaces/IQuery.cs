using MediatR;

namespace SchoolConnect.Common.Application.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse> { }
