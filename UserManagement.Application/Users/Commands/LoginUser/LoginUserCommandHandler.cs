using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Lógica de autenticação...
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null){
                throw new Exception("Credenciais inválidas");
            }
            
            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new Exception("Credenciais inválidas");
            }


            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginUserResponse(user.Id, user.Name, user.Email, token);
        }
    }
}