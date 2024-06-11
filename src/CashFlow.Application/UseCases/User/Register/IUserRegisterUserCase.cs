using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response.User;

namespace CashFlow.Application.UseCases.User.Register;
public interface IUserRegisterUserCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson requestRegisterUserJson);
}
