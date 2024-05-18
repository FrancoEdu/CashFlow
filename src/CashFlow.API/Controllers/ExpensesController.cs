﻿using CashFlow.Application.UseCases.Expenses.Register;
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
    public async Task<IActionResult> Register([FromBody] ExpenseRegisterRequestJson req)
    {
        await _registerExpenseUseCase.Execute(req);   
        return Ok();
    }
}
