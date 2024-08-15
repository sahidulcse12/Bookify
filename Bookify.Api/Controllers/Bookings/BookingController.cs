﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Bookify.Domain.Abstractions;
using Bookify.Application.Bookings.GetBooking;
using Bookify.Application.Bookings.ReserveBooking;

namespace Bookify.Api.Controllers.Bookings
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ISender _sender;
        public BookingController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBookingQuery(id);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ReserveBooking(
            ReserveBookingRequest request,
            CancellationToken cancellationToken)
        {
            var command = new ReserveBookingCommand(
                request.ApartmentId,
                request.UserId,
                request.StartDate,
                request.EndDate);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);
        }
    }
}
