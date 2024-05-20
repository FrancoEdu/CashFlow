using CashFlow.Application.UseCases.Report.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;
[Route("api/report")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IGenerateExpenseReportExcelUseCase _excelGenerateUseCase;
    public ReportController(IGenerateExpenseReportExcelUseCase excelGenerateUseCase)
    {
        _excelGenerateUseCase = excelGenerateUseCase;
    }

    [HttpGet]
    [Route("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromHeader] DateOnly month)
    {
        byte[] file = await _excelGenerateUseCase.Execute(month);

        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, $"report-{month.Month}_{month.Year}.xlsx");
        
        return NoContent();
    }
}
