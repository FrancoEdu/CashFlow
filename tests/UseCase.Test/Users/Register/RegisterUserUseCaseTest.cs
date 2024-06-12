using CashFlow.Application.UseCases.User.Register;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Users;
using CommonTestUtilities.Requests.Users;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCase.Test.Users.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var req = UserRegisterRequestJsonBuilder.Build();
        var useCase = CreateUseCase();

        var res = await useCase.Execute(req);

        res.Should().NotBeNull();
        res.Name.Should().Be(req.Name);
        res.Token.Should().NotBeNullOrWhiteSpace();
    }

    private UserRegisterUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWorkBuilder = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var pwdEncripter = PasswordEncripterBuilder.Build();
        var token = JwtTokenGeneratorBuild.Build();

        return new UserRegisterUseCase(mapper, pwdEncripter, userWriteOnlyRepository, null, unitOfWorkBuilder, token);
    }
}
