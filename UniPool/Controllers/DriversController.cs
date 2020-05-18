using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniPool.Features.Drivers;

namespace UniPool.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriversController(IMediator mediator) => _mediator = mediator;

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> FinishTrip([FromBody] FinishTrip.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Route("[action]")]
        public async Task<IActionResult> GetCurrentTrip([FromQuery] GetCurrentTrip.Query command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Route("[action]")]
        public async Task<IActionResult> GetDriverTrips([FromQuery] GetDriverTrips.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> RegisterTrip([FromBody] RegisterTrip.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> StartTrip([FromBody] StartTrip.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}