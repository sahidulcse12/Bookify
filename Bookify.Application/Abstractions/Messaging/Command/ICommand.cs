using MediatR;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.Abstractions.Messaging.Command
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {

    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {

    }

    public interface IBaseCommand
    {

    }
}
