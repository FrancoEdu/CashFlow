using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.Expenses.Delete;
public class DeleteExpenseRegisterTest
{
    [Fact]
    public void Success()
    {
        // Arrange
        var expenses = ExpenseDeleteRequestJsonBuilder.GenerateListOfExpenses(5);
        var expense = expenses.FirstOrDefault();

        // Act
        expenses.Remove(expense!);
        var wasRemoved = expenses.Contains(expense!);

        // Assert
        wasRemoved.Should().BeFalse();
    }

    [Fact]
    public void ErrorNotFoundExpense()
    {
        // Arrange
        var expenses = ExpenseDeleteRequestJsonBuilder.GenerateListOfExpenses(3);
        var unknownExpense = ExpenseDeleteRequestJsonBuilder.GenerateExpense();

        // Act
        var exists = expenses.Contains(unknownExpense);
        
        // Assert
        exists.Should().BeFalse();
    }
}
