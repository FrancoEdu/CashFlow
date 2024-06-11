using static BCrypt.Net.BCrypt;
using CashFlow.Domain.Security.Criptography;

namespace CashFlow.Infrastructure.Security.Criptographys;
public class Criptography : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        return HashPassword(password);
    }
}
