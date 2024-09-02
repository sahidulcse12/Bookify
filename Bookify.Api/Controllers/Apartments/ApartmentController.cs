﻿using Bookify.Application.Apartments.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Apartments
{
    //[Authorize]
    [Route("api/apartments")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly ISender _sender;
        public ApartmentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> SearchApartments(
            DateOnly startDate,
            DateOnly endDate,
            CancellationToken cancellationToken)
        {
            var query = new SearchApartmentQuery(startDate, endDate);

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                var apartments = result.Value;
                return Ok(apartments);
            }
            else
            {
                // Log the error or handle it according to your application's requirements
                return BadRequest(result.Error);
            }

            //return Ok(result.Value);
        }
    }
}
