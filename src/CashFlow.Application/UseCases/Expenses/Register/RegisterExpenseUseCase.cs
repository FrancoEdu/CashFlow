using CashFlow.Domain.Entities;
using CashFlow.Communication.Requests;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Communication.Response.Expense;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpenseRepository _expenseRepository;
    public RegisterExpenseUseCase(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public ResponseRegisterExpenseJson Execute(ExpenseRegisterRequestJson request)
    {
        Validate(request);

        var expense = new Expense 
        { 
            Amount = request.Amount, 
            Date = request.Date, 
            Description = request.Description, 
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
            Title = request.Title,
        };

        _expenseRepository.Add(expense);
        return new ResponseRegisterExpenseJson();
    }

    #region Private Methods
    private void Validate(ExpenseRegisterRequestJson request)
    {
        var validator = new RegisterExpenseValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
    #endregion Private Methods
}
