using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users.Events
{
    public record UserCreatedDomainEvents(Guid UserId) : IDomainEvent;
}
