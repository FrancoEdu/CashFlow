using CashFlow.Application.UseCases.Report.Excel;
using CashFlow.Application.UseCases.Report.PDF;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;
[Route("api/report")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IGenerateExpenseReportExcelUseCase _excelGenerateUseCase;
    private readonly IGenerateExpenseReportPdfUseCase _pdfGenerateUseCase;
    public ReportController(IGenerateExpenseReportExcelUseCase excelGenerateUseCase, 
        IGenerateExpenseReportPdfUseCase pdfGenerateUseCase)
    {
        _excelGenerateUseCase = excelGenerateUseCase;
        _pdfGenerateUseCase = pdfGenerateUseCase;
    }

    [HttpGet]
    [Route("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromQuery] DateOnly month)
    {
        byte[] file = await _excelGenerateUseCase.Execute(month);

        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, $"report-{month.Month}_{month.Year}.xlsx");
        
        return NoContent();
    }
    
    [HttpGet]
    [Route("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf([FromQuery] DateOnly month)
    {
        byte[] file = await _pdfGenerateUseCase.Execute(month);

        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, $"report-{month.Month}_{month.Year}.pdf");
        
        return NoContent();
    }
}
