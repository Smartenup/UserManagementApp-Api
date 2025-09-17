using FluentValidation;

namespace UserManagement.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório")
                .EmailAddress().WithMessage("Formato de e-mail inválido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória");
        }
    }
}