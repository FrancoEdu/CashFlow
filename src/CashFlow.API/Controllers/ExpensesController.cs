using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route("api/expenses")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly IRegisterExpenseUseCase _registerExpenseUseCase;

    public ExpensesController(IRegisterExpenseUseCase registerExpenseUseCase)
    {
        _registerExpenseUseCase = registerExpenseUseCase;
    }

    [HttpPost]
    public IActionResult Register([FromBody] ExpenseRegisterRequestJson req)
    {
        var response = _registerExpenseUseCase.Execute(req);   
        return Ok(response);
    }
}
