using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniPool.Features.Users;

namespace UniPool.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [Route("[action]")]
        public async Task<IActionResult> LogIn([FromQuery] LogIn.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudent.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}