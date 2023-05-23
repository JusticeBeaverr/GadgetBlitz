using AutoMapper;
using GadgetBlitzZTPAI.Server.Application.Commands;
using GadgetBlitzZTPAI.Server.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GadgetBlitzZTPAI.Server.Controllers
{
    [ApiVersion("1.0")]
    [Route("gadgetblitz/api/v{version:apiVersion}/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO registerDto)
        {
            try
            {
                var command = new RegisterUserCommand(registerDto);
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO loginDto)
        {
            try
            {
                var query = new AuthenticateUserCommand(loginDto);
                var result = await _mediator.Send(query);

                if (!result.Success)
                    return Unauthorized();

                var responseDto = new LoginResponseDTO
                {
                    Token = result.Token,
                    // Additional properties you may want to include in the response
                };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
