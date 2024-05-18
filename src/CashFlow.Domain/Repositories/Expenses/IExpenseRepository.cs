using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpenseRepository
{
    public void Add(Expense expense);
}
