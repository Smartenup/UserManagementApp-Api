using MediatR;

namespace UserManagement.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(string Name, string Email, string Password) : IRequest<Guid>;
}
