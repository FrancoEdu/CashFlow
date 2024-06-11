using CashFlow.Application.UseCases.Login;
using CashFlow.Communication.Requests.Login;
using CashFlow.Communication.Response.Error;
using CashFlow.Communication.Response.Login;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;

    public LoginController(ILoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(LoginResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequestJson req)
    {
        var res = await _loginUseCase.Execute(req);
        return Ok(res);
    }
}
