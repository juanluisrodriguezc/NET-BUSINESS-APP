using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.UseCases.Users.Commands.SignInUser;
using Pacagroup.Ecommerce.Application.UseCases.Users.Commands.SignUpUser;
using Swashbuckle.AspNetCore.Annotations;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v3
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        [SwaggerOperation(Summary = "Registra un nuevo usuario")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpUserCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        [SwaggerOperation(Summary = "Autentica un usuario y genera token")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInUserCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
                return Ok(response);

            return Unauthorized(response);
        }        
    }
}
