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
        public async Task<IActionResult> RegisterTrip([FromBody] RegisterTrip.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}