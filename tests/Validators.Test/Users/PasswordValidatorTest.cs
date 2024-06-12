using CashFlow.Application.UseCases.User;
using CashFlow.Communication.Requests.User;
using FluentAssertions;
using FluentValidation;

namespace Validators.Test.Users;
public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaa1")]
    public void Error_Pwd(string pwd)
    {
        var validator = new PasswordValidator<RequestRegisterUserJson>();

        var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), pwd);

        result.Should().BeFalse();
    }
}
