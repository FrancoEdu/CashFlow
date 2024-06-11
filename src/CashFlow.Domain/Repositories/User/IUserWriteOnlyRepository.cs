using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.User;
public interface IUserWriteOnlyRepository
{
    Task Add(Domain.Entities.User user);
}
