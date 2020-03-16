using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniPool.Features.Passengers;

namespace UniPool.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PassengersController(IMediator mediator) => _mediator = mediator;

        [Route("[action]")]
        public async Task<IActionResult> SearchTrips([FromQuery] SearchTrips.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Route("[action]")]
        public async Task<IActionResult> GetTripInfo([FromQuery] GetTripInfo.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}