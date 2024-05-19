using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Response.Expense;

namespace CashFlow.Application.UseCases.Expenses.Update;
public interface IUpdateExpensesUseCase
{
    Task Execute(long id, ExpenseUpdateRequestJson request);
}
