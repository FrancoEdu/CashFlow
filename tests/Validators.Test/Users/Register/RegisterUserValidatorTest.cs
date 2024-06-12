using CashFlow.Application.UseCases.User.Register;
using CashFlow.Exception;
using CommonTestUtilities.Requests.Users;
using FluentAssertions;

namespace Validators.Test.Users.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = UserRegisterRequestJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Error_Name_Empty(string name)
    {
        var validator = new RegisterUserValidator();
        var request = UserRegisterRequestJsonBuilder.Build();
        request.Name = name;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.NAME_EMPTY));
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Error_Email_Empty(string email)
    {
        var validator = new RegisterUserValidator();
        var request = UserRegisterRequestJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new RegisterUserValidator();
        var request = UserRegisterRequestJsonBuilder.Build();
        request.Email = "Email.comom";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.EMAIL_INVALID));
    }

    [Fact]
    public void Error_Pwd_Empty()
    {
        var validator = new RegisterUserValidator();
        var request = UserRegisterRequestJsonBuilder.Build();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.INVALID_PASSWORD));
    }
}
