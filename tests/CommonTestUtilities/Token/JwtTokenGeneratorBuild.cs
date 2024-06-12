using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Token;
public class JwtTokenGeneratorBuild
{
    public static ITokenGenerator Build()
    {
        var mock = new Mock<ITokenGenerator>();

        mock.Setup(tokenGenerator => tokenGenerator
            .GenerateToken(It.IsAny<User>()))
            .Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

        mock.Setup(tokenGenerator => tokenGenerator
            .ExpiresTime())
            .Returns(DateTime.UtcNow.AddMinutes(1000));

        return mock.Object;
    }
}
