using CashFlow.Communication.Response.Expense;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public interface IGetByIdUseCase
{
    Task<ExpenseResponseJson> Execute(long id);
}
