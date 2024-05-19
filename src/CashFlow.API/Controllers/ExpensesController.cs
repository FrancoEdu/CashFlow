using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response.Error;
using CashFlow.Communication.Response.Expense;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route("api/expenses")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly IRegisterExpenseUseCase _registerExpenseUseCase;
    private readonly IGetAllExpensesUseCase _getAllExpenseUseCase;
    private readonly IGetByIdUseCase _getByIdExpenseUseCase;
    private readonly IDeleteExpenseUseCase _deleteExpenseUseCase;

    public ExpensesController(
        IRegisterExpenseUseCase registerExpenseUseCase,
        IGetAllExpensesUseCase getAllExpenseUseCase,
        IGetByIdUseCase getByIdExpenseUseCase,
        IDeleteExpenseUseCase deleteExpenseUseCase)
    {
        _registerExpenseUseCase = registerExpenseUseCase;
        _getAllExpenseUseCase = getAllExpenseUseCase;
        _getByIdExpenseUseCase = getByIdExpenseUseCase;
        _deleteExpenseUseCase = deleteExpenseUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] ExpenseRegisterRequestJson req)
    {
        await _registerExpenseUseCase.Execute(req);
        return Created();
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetAllExponsesResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll()
    {
        var expenses = await _getAllExpenseUseCase.Execute();
        return expenses.Expense.Any() ? Ok(expenses) : NoContent();
    }

    [HttpGet("{expenseId}")]
    [ProducesResponseType(typeof(ExpenseResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long expenseId)
    {
        var expense = await _getByIdExpenseUseCase.Execute(expenseId);
        return expense != null ? Ok(expense) : NotFound();
    }

    [HttpDelete]
    [Route("{expenseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long expenseId)
    {
        await _deleteExpenseUseCase.Execute(expenseId);
        return NoContent();
    }
}
