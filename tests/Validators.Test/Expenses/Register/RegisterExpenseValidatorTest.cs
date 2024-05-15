using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.Expenses.Register;
public class RegisterExpenseValidatorTest
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = ExpenseRegisterRequestJsonBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ErrorTitleEmpty()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = ExpenseRegisterRequestJsonBuilder.Build();
        request.Title = string.Empty;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.TITLE_IS_REQUIRED));
    }

    [Fact]
    public void ErrorDateGreatherThanToday()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = ExpenseRegisterRequestJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(2);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_DATE_FUTURE));
    }

    [Fact]
    public void ErrorPaymentTypeIsInvalid()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = ExpenseRegisterRequestJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.PAYMENT_TYPE_NOT_VALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-200)]
    public void ErrorAmount(decimal amount)
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = ExpenseRegisterRequestJsonBuilder.Build();
        request.Amount = amount;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.AMOUNT_GREATHER_THAN_ZERO));
    }
}
