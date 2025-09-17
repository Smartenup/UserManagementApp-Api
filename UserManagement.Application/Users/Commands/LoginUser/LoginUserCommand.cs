using MediatR;

namespace UserManagement.Application.Users.Commands.LoginUser
{
    public sealed record LoginUserCommand(
        string Email,
        string Password) : IRequest<LoginUserResponse>;
}
