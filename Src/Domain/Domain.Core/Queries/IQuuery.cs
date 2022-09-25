using MediatR;

namespace Domain.Core.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{ }