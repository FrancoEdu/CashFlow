using CashFlow.Application.UseCases.User.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
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

    [Fact]
    public async Task Error_Name_Empty()
    {
        var req = UserRegisterRequestJsonBuilder.Build();
        req.Name = string.Empty;

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(req);

        var res = await act.Should().ThrowAsync<ErrorOnValidationException>();
        res.Where(x => x.GetErrors().Count == 1 && x.GetErrors().Contains(ResourceErrorMessage.NAME_EMPTY)); 
    }

    private UserRegisterUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWorkBuilder = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var pwdEncripter = PasswordEncripterBuilder.Build();
        var token = JwtTokenGeneratorBuild.Build();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder().Build();

        return new UserRegisterUseCase(mapper, pwdEncripter, userWriteOnlyRepository, userReadOnlyRepository, unitOfWorkBuilder, token);
    }
}
