using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Models;
using UserManagement.Application.Users.Commands.CreateUser;
using UserManagement.Application.Users.Queries;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateUserResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
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

                if (request.Password != request.ConfirmPassword)
                {
                    return BadRequest(ApiResponse<object>.Fail("As senhas não coincidem",
                        new List<string> { "As senhas fornecidas não coincidem" }));
                }

                var command = new CreateUserCommand(request.Name, request.Email, request.Password);
                var result = await _mediator.Send(command);

                var response = new CreateUserResponse(
                    result,
                    request.Name,
                    request.Email,
                    DateTime.UtcNow);

                return CreatedAtAction(nameof(GetUsers), new { id = result },
                    ApiResponse<CreateUserResponse>.Ok(response, "Usuário criado com sucesso"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.Fail("Erro interno do servidor",
                        new List<string> { ex.Message }));
            }
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<ListUserResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetUsers()
        {

            try
            {
                var query = new GetUsersQuery();
                var users = await _mediator.Send(query);

                var usrListResponse = new ListUserResponse();

                foreach (var user in users)
                {
                    usrListResponse.Users.Add(new UserManagement.API.Models.UserResponse(user.Id, user.Name, user.Email, user.CreatedAt));
                }
                return Ok(ApiResponse<ListUserResponse>.Ok(usrListResponse, "Lista de usuários consultada com sucesso"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar usuários");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.Fail("Erro interno do servidor",
                        new List<string> { ex.Message }));
            }
        }
    }
}