namespace UserManagement.API.Models
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public LoginResponse(Guid id, string name, string email, string token)
        {
            Id = id;
            Name = name;
            Email = email;
            Token = token;
        }
    }
}