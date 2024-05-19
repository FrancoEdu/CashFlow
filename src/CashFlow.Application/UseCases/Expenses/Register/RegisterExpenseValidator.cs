using FluentValidation;
using CashFlow.Exception;
using CashFlow.Communication.Requests.Expense;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseValidator : AbstractValidator<ExpenseRegisterRequestJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage(ResourceErrorMessage.TITLE_IS_REQUIRED);
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage(ResourceErrorMessage.AMOUNT_GREATHER_THAN_ZERO);
        RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessage.EXPENSE_DATE_FUTURE);
        RuleFor(x => x.PaymentType).IsInEnum().WithMessage(ResourceErrorMessage.PAYMENT_TYPE_NOT_VALID);
    }
}
