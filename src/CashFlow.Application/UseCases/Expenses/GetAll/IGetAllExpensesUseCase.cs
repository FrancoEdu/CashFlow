using CashFlow.Communication.Response.Expense;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public interface IGetAllExpensesUseCase
{
    public Task<GetAllExponsesResponseJson> Execute();
}
