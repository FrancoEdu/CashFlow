using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Response.Expense;

namespace CashFlow.Application.UseCases.Expenses.Register;
public interface IRegisterExpenseUseCase
{
    public Task<ResponseRegisterExpenseJson> Execute(ExpenseRegisterRequestJson request);
}
