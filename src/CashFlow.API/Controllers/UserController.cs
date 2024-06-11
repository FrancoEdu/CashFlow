using CashFlow.Application.UseCases.User.Register;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response.Error;
using CashFlow.Communication.Response.User;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRegisterUserCase _userRegisterUserCase;

    public UserController(IUserRegisterUserCase userRegisterUserCase)
    {
        _userRegisterUserCase = userRegisterUserCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RequestRegisterUserJson req)
    {
        var res = await _userRegisterUserCase.Execute(req);
        return Created(string.Empty, res);
    }
}
