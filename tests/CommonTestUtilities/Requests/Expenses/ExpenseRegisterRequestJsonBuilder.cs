using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Expense;

namespace CommonTestUtilities.Requests.Expenses;
public class ExpenseRegisterRequestJsonBuilder
{
    public static ExpenseRegisterRequestJson Build()
    {
        return new Faker<ExpenseRegisterRequestJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal());
    }
}
