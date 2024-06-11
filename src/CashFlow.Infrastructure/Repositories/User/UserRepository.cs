using CashFlow.Domain.Repositories.User;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Repositories.User;
internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly CashFlowDbContext _context;

    public UserRepository(CashFlowDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.User user)
    {
        await _context.User.AddAsync(user);
    }

    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
        return await _context
            .User
            .AsNoTracking()
            .AnyAsync(x => x.Email.ToLower().Equals(email.ToLower()));
    }

    public async Task<Domain.Entities.User?> FindByEmail(string email)
    {
        return await _context
            .User
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));
    }
}
