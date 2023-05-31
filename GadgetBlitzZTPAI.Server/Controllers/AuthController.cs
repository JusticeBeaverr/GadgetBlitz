using AutoMapper;
using GadgetBlitzZTPAI.Server.Application.Commands;
using GadgetBlitzZTPAI.Server.Application.DTO;
using GadgetBlitzZTPAI.Server.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

                if (result == null)
                {
                    return Unauthorized(); // Zwracamy kod 401 Unauthorized, gdy logowanie nie powiodło się
                }

                var responseDto = new LoginResponseDTO(result.UserId, result.Username, result.Email,result.Role, result.Token);

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("users"), Authorize(Roles = "Admin")]
        
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);

            var smartphones = result.Items;
            return Ok(smartphones);

        }

        [HttpDelete("userid"), Authorize]
        public async Task<ActionResult> DeleteUserById(Guid id)
        {
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("changepassword"), Authorize(Roles = "User")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            try
            {
                var command = new ChangePasswordCommand(changePassword);
                var result = await _mediator.Send(command);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Nieudana zmiana hasła.");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
