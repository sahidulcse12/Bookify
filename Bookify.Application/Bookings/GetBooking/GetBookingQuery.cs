using Bookify.Application.Abstractions.Messaging.Queries;

namespace Bookify.Application.Bookings.GetBooking
{
    public sealed record GetBookingQuery(Guid bookingId) : IQuery<BookingResponse>;
}
