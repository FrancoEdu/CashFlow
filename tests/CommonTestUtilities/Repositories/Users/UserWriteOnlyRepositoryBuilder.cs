using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories.Users;
public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        return new Mock<IUserWriteOnlyRepository>().Object;
    }
}
