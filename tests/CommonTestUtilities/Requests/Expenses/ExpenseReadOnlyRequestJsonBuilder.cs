using Bogus;
using CashFlow.Communication.Enums;
using CommonTestUtilities.Jsons;

namespace CommonTestUtilities.Requests.Expenses;
public class ExpenseReadOnlyRequestJsonBuilder
{
    public static IEnumerable<ExpenseRegisterJson> GenerateListOfExpenses(int lenghtList)
    {
        List<ExpenseRegisterJson> list = new List<ExpenseRegisterJson>();

        for (int i = 0; i < lenghtList; i++)
        {
            var newObj = new Faker<ExpenseRegisterJson>()
                .RuleFor(r => r.Id, faker => faker.UniqueIndex)
                .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
                .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
                .RuleFor(r => r.Date, faker => faker.Date.Past())
                .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
                .RuleFor(r => r.Amount, faker => faker.Random.Decimal());
            list.Add(newObj);
        }
        return list;
    }

    public static ExpenseRegisterJson GenerateExpense()
    {
        return new Faker<ExpenseRegisterJson>()
            .RuleFor(r => r.Id, faker => faker.UniqueIndex)
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal());
    }
}
