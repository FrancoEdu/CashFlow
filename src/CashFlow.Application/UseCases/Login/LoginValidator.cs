using CashFlow.Application.UseCases.User;
using CashFlow.Communication.Requests.Login;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Login;
public class LoginValidator : AbstractValidator<LoginRequestJson>
{
    public LoginValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessage.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourceErrorMessage.EMAIL_INVALID);

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<LoginRequestJson>());
    }
}
