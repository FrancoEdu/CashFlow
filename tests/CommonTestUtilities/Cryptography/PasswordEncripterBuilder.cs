using CashFlow.Domain.Security.Criptography;
using Moq;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        var mock = new Mock<IPasswordEncripter>();

        mock.Setup(pwdEncripter => pwdEncripter
            .Encrypt(It.IsAny<string>()))
            .Returns("!Aa1ereeSDS");

        return mock.Object;
    }
}
