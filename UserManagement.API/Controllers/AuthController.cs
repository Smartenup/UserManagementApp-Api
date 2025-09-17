using Microsoft.AspNetCore.Mvc;
using MediatR;
using UserManagement.API.Models;
using UserManagement.Application.Users.Commands.LoginUser;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(ApiResponse<object>.Fail("Dados inválidos", errors));
                }

                var command = new LoginUserCommand(request.Email, request.Password);
                var result = await _mediator.Send(command);

                var response = new LoginResponse(
                    result.Id,
                    result.Name,
                    result.Email,
                    result.Token);

                return Ok(ApiResponse<LoginResponse>.Ok(response, "Login realizado com sucesso"));
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Tentativa de login falhou para o email: {Email}", request.Email);
                return Unauthorized(ApiResponse<object>.Fail("Credenciais inválidas", new List<string> { ex.Message }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o login para o email: {Email}", request.Email);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.Fail("Erro interno do servidor", new List<string> { ex.Message }));
            }
        }
    }
}