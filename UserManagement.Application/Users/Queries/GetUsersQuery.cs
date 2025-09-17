using MediatR;

namespace UserManagement.Application.Users.Queries
{
    public record GetUsersQuery : IRequest<List<UserResponse>>;
}
