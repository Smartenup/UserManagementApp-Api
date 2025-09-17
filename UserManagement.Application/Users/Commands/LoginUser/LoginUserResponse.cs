namespace UserManagement.Application.Users.Commands.LoginUser
{
    public sealed record LoginUserResponse(
        Guid Id,
        string Name,
        string Email,
        string Token);
}