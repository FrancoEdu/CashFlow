using Bogus;
using CashFlow.Communication.Requests.User;
using System.Net.NetworkInformation;

namespace CommonTestUtilities.Requests.Users;
public class UserRegisterRequestJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(u => u.Name, faker => faker.Person.FirstName)
            .RuleFor(u => u.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(u => u.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}
