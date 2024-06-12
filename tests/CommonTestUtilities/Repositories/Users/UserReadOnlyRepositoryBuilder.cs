using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories.Users;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;

    public UserReadOnlyRepositoryBuilder()
    {
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
    }

    public IUserReadOnlyRepository Build() => _userReadOnlyRepositoryMock.Object;
}
