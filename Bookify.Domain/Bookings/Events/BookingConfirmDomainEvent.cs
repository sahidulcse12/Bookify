using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events
{
    public record BookingConfirmDomainEvent(Guid id): IDomainEvent;
}
