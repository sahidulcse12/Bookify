using Bookify.Application.Abstractions.Clock;

namespace Bookify.Infrustructure.Clock
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
