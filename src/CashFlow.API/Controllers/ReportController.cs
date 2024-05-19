using CashFlow.Communication.Requests.Report;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;
[Route("api/report")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet]
    [Route("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromHeader] DateOnly month)
    {
        byte[] file = new byte[1];

        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        
        return NoContent();
    }
}
