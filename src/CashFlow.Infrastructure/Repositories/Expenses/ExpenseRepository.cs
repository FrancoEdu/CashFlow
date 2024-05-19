using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Repositories.Expenses;
internal class ExpenseRepository : IExpenseWriteOnlyRepository, IExpenseReadOnlyRepository, IExpenseUpdateOnlyRepository
{
    private readonly CashFlowDbContext _context;

    public ExpenseRepository(CashFlowDbContext context)
    {
        _context = context;
    }

    public async Task Add(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (result is null)
        {
            return false;
        }
        _context.Expenses.Remove(result);
        return true;
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _context.Expenses.AsNoTracking().ToListAsync();
    }

    public async Task<Expense?> GetById(long id)
    {
        return await _context.Expenses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Expense?> GetByIdWithOutAsNoTracking(long id)
    {
       return await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Update(Expense expense)
    {
        _context.Expenses.Update(expense);
    }
}
