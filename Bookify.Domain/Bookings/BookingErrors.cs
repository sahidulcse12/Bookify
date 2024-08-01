using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings
{
    public static class BookingErrors
    {
        public static Error NotFound = new(
            "Booking.Found",
            "the booking of the specified id was not found"
            );

        public static Error Overlap = new(
            "Booking.Overlap",
            "the current booking is overlapping with an existing one"
            );

        public static Error NotReserved = new(
            "Booking.Notreserved",
            "the current booking is not pending"
            );

        public static Error NotConfirmed = new(
            "Booking.NotConfirmed",
            "the current booking is not confirmed"
            );

        public static Error AlreadyStarted = new(
            "Booking.AlreadyStarted",
            "the booking has already started"
            );
    }
}
