using Bookify.Application.Abstractions.Messaging.Queries;

namespace Bookify.Application.Apartments.SearchApartments
{
    public sealed record SearchApartmentQuery(
        DateOnly StartDate,
        DateOnly EndDate): IQuery<IReadOnlyList<ApartmentResponse>>;
}
