using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpenseReadOnlyRepository
{
    Task<List<Expense>> GetAll();
    Task<List<Expense>> GetAllExpensesByMonth(DateOnly month);
    Task<Expense?> GetById(long id);
    Task<Expense?> GetByIdWithOutAsNoTracking(long id);
}
