using FluentValidation;
using Questao5.Application.Commands.Requests;

namespace Questao5.Presentation.Validators
{
    public class AccountValidator : AbstractValidator<AccountCommand>
    {
        public AccountValidator()
        {
            RuleFor(x=>x.Conta).GreaterThan(0).WithMessage("A conta deve ser maior que zero!");
        }
    }
}
