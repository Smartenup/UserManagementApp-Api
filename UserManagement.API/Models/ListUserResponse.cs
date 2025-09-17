namespace UserManagement.API.Models
{

    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public UserResponse(Guid id, string name, string email, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Email = email;
            CreatedAt = createdAt;
        }
    }
    public class ListUserResponse
    {
        public List<UserResponse> Users { get; set; } = new List<UserResponse>();
    }
}