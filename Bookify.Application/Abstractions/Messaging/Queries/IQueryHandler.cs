using MediatR;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.Abstractions.Messaging.Queries
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
