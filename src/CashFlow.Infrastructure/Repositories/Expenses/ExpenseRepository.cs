using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Infrastructure.Repositories.Expenses;
internal class ExpenseRepository : IExpenseRepository
{
    private readonly CashFlowDbContext _context;

    public ExpenseRepository(CashFlowDbContext context)
    {
        _context = context;
    }

    public void Add(Expense expense)
    {
        _context.Add(expense);
        _context.SaveChanges();
    }
}
