using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Security.Tokens;
public interface ITokenGenerator
{
    string GenerateToken(User user);
    DateTime ExpiresTime();
}
