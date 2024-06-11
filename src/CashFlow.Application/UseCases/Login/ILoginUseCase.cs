using CashFlow.Communication.Requests.Login;
using CashFlow.Communication.Response.Login;

namespace CashFlow.Application.UseCases.Login;
public interface ILoginUseCase
{
    Task<LoginResponseJson> Execute(LoginRequestJson loginRequest);
}
