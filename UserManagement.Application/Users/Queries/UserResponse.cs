namespace UserManagement.Application.Users.Queries
{
    public record UserResponse(Guid Id, string Name, string Email, DateTime CreatedAt);
}
